using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.ExistingDb.Models;

namespace EFGetStarted.AspNetCore.ExistingDb.Controllers
{
    public class VærktøjskasseController : Controller
    {
        private readonly CraftManDBContext _context;

        public VærktøjskasseController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: Værktøjskasse
        public async Task<IActionResult> Index()
        {
            var craftManDBContext = _context.Værktøjskasse.Include(v => v.EjesAfNavigation);
            return View(await craftManDBContext.ToListAsync());
        }

        // GET: Værktøjskasse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var værktøjskasse = await _context.Værktøjskasse
                .Include(v => v.EjesAfNavigation)
                .FirstOrDefaultAsync(m => m.VkasseId == id);
            if (værktøjskasse == null)
            {
                return NotFound();
            }

            return View(værktøjskasse);
        }

        // GET: Værktøjskasse/Create
        public IActionResult Create()
        {
            ViewData["EjesAf"] = new SelectList(_context.Håndværker, "HåndværkerId", "Efternavn");
            return View();
        }

        // POST: Værktøjskasse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VkasseId,Anskaffet,Fabrikat,EjesAf,Model,Serienummer,Farve")] Værktøjskasse værktøjskasse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(værktøjskasse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EjesAf"] = new SelectList(_context.Håndværker, "HåndværkerId", "Efternavn", værktøjskasse.EjesAf);
            return View(værktøjskasse);
        }

        // GET: Værktøjskasse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var værktøjskasse = await _context.Værktøjskasse.FindAsync(id);
            if (værktøjskasse == null)
            {
                return NotFound();
            }
            ViewData["EjesAf"] = new SelectList(_context.Håndværker, "HåndværkerId", "Efternavn", værktøjskasse.EjesAf);
            return View(værktøjskasse);
        }

        // POST: Værktøjskasse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VkasseId,Anskaffet,Fabrikat,EjesAf,Model,Serienummer,Farve")] Værktøjskasse værktøjskasse)
        {
            if (id != værktøjskasse.VkasseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(værktøjskasse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VærktøjskasseExists(værktøjskasse.VkasseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EjesAf"] = new SelectList(_context.Håndværker, "HåndværkerId", "Efternavn", værktøjskasse.EjesAf);
            return View(værktøjskasse);
        }

        // GET: Værktøjskasse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var værktøjskasse = await _context.Værktøjskasse
                .Include(v => v.EjesAfNavigation)
                .FirstOrDefaultAsync(m => m.VkasseId == id);
            if (værktøjskasse == null)
            {
                return NotFound();
            }

            return View(værktøjskasse);
        }

        // POST: Værktøjskasse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var værktøjskasse = await _context.Værktøjskasse.FindAsync(id);
            _context.Værktøjskasse.Remove(værktøjskasse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VærktøjskasseExists(int id)
        {
            return _context.Værktøjskasse.Any(e => e.VkasseId == id);
        }
    }
}
