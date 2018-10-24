using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.ExistingDb.Models;

namespace EFGetStarted.AspNetCore.ExistingDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Værktøjskasse1Controller : ControllerBase
    {
        private readonly CraftManDBContext _context;

        public Værktøjskasse1Controller(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: api/Værktøjskasse1
        [HttpGet]
        public IEnumerable<Værktøjskasse> GetVærktøjskasse()
        {
            return _context.Værktøjskasse;
        }

        // GET: api/Værktøjskasse1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVærktøjskasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøjskasse = await _context.Værktøjskasse.FindAsync(id);

            if (værktøjskasse == null)
            {
                return NotFound();
            }

            return Ok(værktøjskasse);
        }

        // PUT: api/Værktøjskasse1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVærktøjskasse([FromRoute] int id, [FromBody] Værktøjskasse værktøjskasse)
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

        // POST: api/Værktøjskasse1
        [HttpPost]
        public async Task<IActionResult> PostVærktøjskasse([FromBody] Værktøjskasse værktøjskasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Værktøjskasse.Add(værktøjskasse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVærktøjskasse", new { id = værktøjskasse.VkasseId }, værktøjskasse);
        }

        // DELETE: api/Værktøjskasse1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVærktøjskasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøjskasse = await _context.Værktøjskasse.FindAsync(id);
            if (værktøjskasse == null)
            {
                return NotFound();
            }

            _context.Værktøjskasse.Remove(værktøjskasse);
            await _context.SaveChangesAsync();

            return Ok(værktøjskasse);
        }

        private bool VærktøjskasseExists(int id)
        {
            return _context.Værktøjskasse.Any(e => e.VkasseId == id);
        }
    }
}