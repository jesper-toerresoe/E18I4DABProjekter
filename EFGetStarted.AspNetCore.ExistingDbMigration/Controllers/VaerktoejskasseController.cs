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
    public class VaerktoejskasseController : ControllerBase
    {
        private readonly Database5Context _context;

        public VaerktoejskasseController(Database5Context context)
        {
            _context = context;
        }

        // GET: api/Vaerktoejskasse
        [HttpGet]
        public IEnumerable<Vaerktoejskasse> GetVaerktoejskasse()
        {
            return _context.Vaerktoejskasse;
        }

        // GET: api/Vaerktoejskasse/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVaerktoejskasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);

            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return Ok(vaerktoejskasse);
        }

        // PUT: api/Vaerktoejskasse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVaerktoejskasse([FromRoute] int id, [FromBody] Vaerktoejskasse vaerktoejskasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaerktoejskasse.VTKId)
            {
                return BadRequest();
            }

            _context.Entry(vaerktoejskasse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaerktoejskasseExists(id))
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
        public async Task<IActionResult> PostVaerktoejskasse([FromBody] Vaerktoejskasse vaerktoejskasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vaerktoejskasse.Add(vaerktoejskasse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVaerktoejskasse", new { id = vaerktoejskasse.VTKId }, vaerktoejskasse);
        }

        // DELETE: api/Vaerktoejskasse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaerktoejskasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            _context.Vaerktoejskasse.Remove(vaerktoejskasse);
            await _context.SaveChangesAsync();

            return Ok(vaerktoejskasse);
        }

        private bool VaerktoejskasseExists(int id)
        {
            return _context.Vaerktoejskasse.Any(e => e.VTKId == id);
        }
    }
}