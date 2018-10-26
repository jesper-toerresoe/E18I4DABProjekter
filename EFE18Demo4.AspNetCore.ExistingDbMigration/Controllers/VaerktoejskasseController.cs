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
    public class VaerktoejskasseController : ControllerBase
    {
        private readonly CraftManDBContext _context;

        public VaerktoejskasseController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: api/Vaerktoejskasse
        [HttpGet]
        public IEnumerable<Vaerktoejskasse> GetVærktøjskasse()
        {
            return _context.Vaerktoejskasse;
        }

        // GET: api/Vaerktoejskasse/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVærktøjskasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøjskasse = await _context.Vaerktoejskasse.FindAsync(id);

            if (værktøjskasse == null)
            {
                return NotFound();
            }

            return Ok(værktøjskasse);
        }

        // PUT: api/Vaerktoejskasse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVærktøjskasse([FromRoute] int id, [FromBody] Vaerktoejskasse værktøjskasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != værktøjskasse.VkasseId)
            {
                return BadRequest();
            }

            _context.Entry(værktøjskasse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VærktøjskasseExists(id))
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

        // POST: api/Vaerktoejskasse
        [HttpPost]
        public async Task<IActionResult> PostVærktøjskasse([FromBody] Vaerktoejskasse værktøjskasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vaerktoejskasse.Add(værktøjskasse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVærktøjskasse", new { id = værktøjskasse.VkasseId }, værktøjskasse);
        }

        // DELETE: api/Vaerktoejskasse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVærktøjskasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøjskasse = await _context.Vaerktoejskasse.FindAsync(id);
            if (værktøjskasse == null)
            {
                return NotFound();
            }

            _context.Vaerktoejskasse.Remove(værktøjskasse);
            await _context.SaveChangesAsync();

            return Ok(værktøjskasse);
        }

        private bool VærktøjskasseExists(int id)
        {
            return _context.Vaerktoejskasse.Any(e => e.VkasseId == id);
        }
    }
}