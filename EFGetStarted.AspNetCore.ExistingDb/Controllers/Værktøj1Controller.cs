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
    public class Værktøj1Controller : ControllerBase
    {
        private readonly CraftManDBContext _context;

        public Værktøj1Controller(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: api/Værktøj1
        [HttpGet]
        public IEnumerable<Værktøj> GetVærktøj()
        {
            return _context.Værktøj;
        }

        // GET: api/Værktøj1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVærktøj([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøj = await _context.Værktøj.FindAsync(id);

            if (værktøj == null)
            {
                return NotFound();
            }

            return Ok(værktøj);
        }

        // PUT: api/Værktøj1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVærktøj([FromRoute] long id, [FromBody] Værktøj værktøj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != værktøj.VærktøjsId)
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

        // POST: api/Værktøj1
        [HttpPost]
        public async Task<IActionResult> PostVærktøj([FromBody] Værktøj værktøj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Værktøj.Add(værktøj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVærktøj", new { id = værktøj.VærktøjsId }, værktøj);
        }

        // DELETE: api/Værktøj1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVærktøj([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var værktøj = await _context.Værktøj.FindAsync(id);
            if (værktøj == null)
            {
                return NotFound();
            }

            _context.Værktøj.Remove(værktøj);
            await _context.SaveChangesAsync();

            return Ok(værktøj);
        }

        private bool VærktøjExists(long id)
        {
            return _context.Værktøj.Any(e => e.VærktøjsId == id);
        }
    }
}