using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFE18Demo.AspNetCore.ExistingDbMigration.Models;

namespace EFE18Demo.AspNetCore.ExistingDbMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HåndværkerController : ControllerBase
    {
        private readonly CraftManDBContext _context;

        public HåndværkerController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: api/Håndværker
        [HttpGet]
        public IEnumerable<Håndværker> GetHåndværker()
        {
            return _context.Håndværker;
        }

        // GET: api/Håndværker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHåndværker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var håndværker = await _context.Håndværker.FindAsync(id);

            if (håndværker == null)
            {
                return NotFound();
            }

            return Ok(håndværker);
        }

        // PUT: api/Håndværker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHåndværker([FromRoute] int id, [FromBody] Håndværker håndværker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != håndværker.HåndværkerId)
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

        // POST: api/Håndværker
        [HttpPost]
        public async Task<IActionResult> PostHåndværker([FromBody] Håndværker håndværker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Håndværker.Add(håndværker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHåndværker", new { id = håndværker.HåndværkerId }, håndværker);
        }

        // DELETE: api/Håndværker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHåndværker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var håndværker = await _context.Håndværker.FindAsync(id);
            if (håndværker == null)
            {
                return NotFound();
            }

            _context.Håndværker.Remove(håndværker);
            await _context.SaveChangesAsync();

            return Ok(håndværker);
        }

        private bool HåndværkerExists(int id)
        {
            return _context.Håndværker.Any(e => e.HåndværkerId == id);
        }
    }
}