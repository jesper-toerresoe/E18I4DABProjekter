using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFE18Demo4.AspNetCore.ExistingDbMigration.Models;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Controllers
{
    public class VaerktoejskasseAppController : Controller
    {
        private readonly CraftManDBContext _context;

        public VaerktoejskasseAppController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: VaerktoejskasseApp
        public async Task<IActionResult> Index()
        {
            var craftManDBContext = _context.Vaerktoejskasse.Include(v => v.Haandvaerker);
            return View(await craftManDBContext.ToListAsync());
        }

        // GET: VaerktoejskasseApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse
                .Include(v => v.Haandvaerker)
                .FirstOrDefaultAsync(m => m.VkasseId == id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return View(vaerktoejskasse);
        }

        // GET: VaerktoejskasseApp/Create
        public IActionResult Create()
        {
            ViewData["HaandvaerkerId"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn");
            return View();
        }

        // POST: VaerktoejskasseApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VkasseId,Vtkanskaffet,Vtkfabrikat,HaandvaerkerId,Vtkmodel,Vtkserienummer,Vtkfarve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaerktoejskasse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HaandvaerkerId"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn", vaerktoejskasse.HaandvaerkerId);
            return View(vaerktoejskasse);
        }

        // GET: VaerktoejskasseApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }
            ViewData["HaandvaerkerId"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn", vaerktoejskasse.HaandvaerkerId);
            return View(vaerktoejskasse);
        }

        // POST: VaerktoejskasseApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VkasseId,Vtkanskaffet,Vtkfabrikat,HaandvaerkerId,Vtkmodel,Vtkserienummer,Vtkfarve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (id != vaerktoejskasse.VkasseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaerktoejskasse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaerktoejskasseExists(vaerktoejskasse.VkasseId))
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
            ViewData["HaandvaerkerId"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn", vaerktoejskasse.HaandvaerkerId);
            return View(vaerktoejskasse);
        }

        // GET: VaerktoejskasseApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse
                .Include(v => v.Haandvaerker)
                .FirstOrDefaultAsync(m => m.VkasseId == id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return View(vaerktoejskasse);
        }

        // POST: VaerktoejskasseApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaerktoejskasse = await _context.Vaerktoejskasse.FindAsync(id);
            _context.Vaerktoejskasse.Remove(vaerktoejskasse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaerktoejskasseExists(int id)
        {
            return _context.Vaerktoejskasse.Any(e => e.VkasseId == id);
        }
    }
}
