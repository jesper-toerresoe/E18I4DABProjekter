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
    public class VaerktoejController : ControllerBase
    {
        private readonly CraftManDBContext _context;

        public VaerktoejController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: api/Vaerktoej
        [HttpGet]
        public IEnumerable<Vaerktoej> GetVærktøj()
        {
            return _context.Vaerktoej;
        }

        // GET: api/Vaerktoej/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVærktøj([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøj = await _context.Vaerktoej.FindAsync(id);

            if (værktøj == null)
            {
                return NotFound();
            }

            return Ok(værktøj);
        }

        // PUT: api/Vaerktoej/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVærktøj([FromRoute] long id, [FromBody] Vaerktoej værktøj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != værktøj.VaerktoejsId)
            {
                return BadRequest();
            }

            _context.Entry(værktøj).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VærktøjExists(id))
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

        // POST: api/Vaerktoej
        [HttpPost]
        public async Task<IActionResult> PostVærktøj([FromBody] Vaerktoej værktøj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vaerktoej.Add(værktøj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVærktøj", new { id = værktøj.VaerktoejsId }, værktøj);
        }

        // DELETE: api/Vaerktoej/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVærktøj([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøj = await _context.Vaerktoej.FindAsync(id);
            if (værktøj == null)
            {
                return NotFound();
            }

            _context.Vaerktoej.Remove(værktøj);
            await _context.SaveChangesAsync();

            return Ok(værktøj);
        }

        private bool VærktøjExists(long id)
        {
            return _context.Vaerktoej.Any(e => e.VaerktoejsId == id);
        }
    }
}