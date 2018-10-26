using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFE18Demo4.AspNetCore.ExistingDbMigration.Models;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaandvaerkerController : ControllerBase
    {
        private readonly CraftManDBContext _context;

        public HaandvaerkerController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: api/Haandvaerker
        [HttpGet]
        public IEnumerable<Haandvaerker> GetHåndværker()
        {
            return _context.Haandvaerker;
        }

        // GET: api/Haandvaerker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHåndværker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var håndværker = await _context.Haandvaerker.FindAsync(id);

            if (håndværker == null)
            {
                return NotFound();
            }

            return Ok(håndværker);
        }

        // PUT: api/Haandvaerker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHåndværker([FromRoute] int id, [FromBody] Haandvaerker håndværker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != håndværker.HaandvaerkerId)
            {
                return BadRequest();
            }

            _context.Entry(håndværker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HåndværkerExists(id))
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
        public async Task<IActionResult> PostHåndværker([FromBody] Haandvaerker håndværker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Haandvaerker.Add(håndværker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHåndværker", new { id = håndværker.HaandvaerkerId }, håndværker);
        }

        // DELETE: api/Haandvaerker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHåndværker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var håndværker = await _context.Haandvaerker.FindAsync(id);
            if (håndværker == null)
            {
                return NotFound();
            }

            _context.Haandvaerker.Remove(håndværker);
            await _context.SaveChangesAsync();

            return Ok(håndværker);
        }

        private bool HåndværkerExists(int id)
        {
            return _context.Haandvaerker.Any(e => e.HaandvaerkerId == id);
        }
    }
}