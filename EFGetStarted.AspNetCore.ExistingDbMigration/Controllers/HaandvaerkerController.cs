using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.ExistingDbMigration.Models;

namespace EFGetStarted.AspNetCore.ExistingDbMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaandvaerkerController : ControllerBase
    {
        private readonly Database5Context _context;

        public HaandvaerkerController(Database5Context context)
        {
            _context = context;
        }

        // GET: api/Haandvaerker
        [HttpGet]
        public IEnumerable<Haandvaerker> GetHaandvaerker()
        {
            return  _context.Haandvaerker.Include(h => h.Vaerktoejskasse).ThenInclude(v => v.Vaerktoej);
        }

        // GET: api/Haandvaerker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHaandvaerker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            var haandvaerker = await _context.Haandvaerker.Where(b => b.HaandvaerkerId == id).
                Include(vtk => vtk.Vaerktoejskasse).
                ThenInclude(v => v.Vaerktoej).ToListAsync();
        
            if (haandvaerker == null)
            {
                return NotFound();
            }

            return Ok(haandvaerker);
        }

        // PUT: api/Haandvaerker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHaandvaerker([FromRoute] int id, [FromBody] Haandvaerker haandvaerker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != haandvaerker.HaandvaerkerId)
            {
                return BadRequest();
            }

            _context.Entry(haandvaerker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HaandvaerkerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Haandvaerker
        [HttpPost]
        public async Task<IActionResult> PostHaandvaerker([FromBody] Haandvaerker haandvaerker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Haandvaerker.Add(haandvaerker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHaandvaerker", new { id = haandvaerker.HaandvaerkerId }, haandvaerker);
        }

        // DELETE: api/Haandvaerker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHaandvaerker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            if (haandvaerker == null)
            {
                return NotFound();
            }

            _context.Haandvaerker.Remove(haandvaerker);
            await _context.SaveChangesAsync();

            return Ok(haandvaerker);
        }

        private bool HaandvaerkerExists(int id)
        {
            return _context.Haandvaerker.Any(e => e.HaandvaerkerId == id);
        }
    }
}