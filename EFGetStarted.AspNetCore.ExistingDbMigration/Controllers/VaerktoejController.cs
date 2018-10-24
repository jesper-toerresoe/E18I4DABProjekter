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
    public class VaerktoejController : ControllerBase
    {
        private readonly Database5Context _context;

        public VaerktoejController(Database5Context context)
        {
            _context = context;
        }

        // GET: api/Vaerktoej
        [HttpGet]
        public IEnumerable<Vaerktoej> GetVaerktoej()
        {
            return _context.Vaerktoej;
        }

        // GET: api/Vaerktoej/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaerktoej([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vaerktoej = await _context.Vaerktoej.FindAsync(id);

            if (vaerktoej == null)
            {
                return NotFound();
            }

            return Ok(vaerktoej);
        }

        // PUT: api/Vaerktoej/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaerktoej([FromRoute] long id, [FromBody] Vaerktoej vaerktoej)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaerktoej.VTId)
            {
                return BadRequest();
            }

            _context.Entry(vaerktoej).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaerktoejExists(id))
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
        public async Task<IActionResult> PostVaerktoej([FromBody] Vaerktoej vaerktoej)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vaerktoej.Add(vaerktoej);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaerktoej", new { id = vaerktoej.VTId }, vaerktoej);
        }

        // DELETE: api/Vaerktoej/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaerktoej([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            _context.Vaerktoej.Remove(vaerktoej);
            await _context.SaveChangesAsync();

            return Ok(vaerktoej);
        }

        private bool VaerktoejExists(long id)
        {
            return _context.Vaerktoej.Any(e => e.VTId == id);
        }
    }
}