using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.AspNetCore.ExistingDb.Models1;

namespace EFGetStarted.AspNetCore.ExistingDb.Controllers
{
    public class VaerktoejskassesAppController : Controller
    {
        private readonly Database3Context _context;

        public VaerktoejskassesAppController(Database3Context context)
        {
            _context = context;
        }

        // GET: VaerktoejskassesApp
        public async Task<IActionResult> Index()
        {
            var database3Context = _context.Vaerktoejskasse.Include(v => v.EjesAfNavigation);
            return View(await database3Context.ToListAsync());
        }

        // GET: VaerktoejskassesApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse
                .Include(v => v.EjesAfNavigation)
                .FirstOrDefaultAsync(m => m.VkasseId == id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return View(vaerktoejskasse);
        }

        // GET: VaerktoejskassesApp/Create
        public IActionResult Create()
        {
            ViewData["EjesAf"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn");
            return View();
        }

        // POST: VaerktoejskassesApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VkasseId,Anskaffet,Fabrikat,EjesAf,Model,Serienummer,Farve")] Vaerktoejskasse vaerktoejskasse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaerktoejskasse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EjesAf"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn", vaerktoejskasse.EjesAf);
            return View(vaerktoejskasse);
        }

        // GET: VaerktoejskassesApp/Edit/5
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
            ViewData["EjesAf"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn", vaerktoejskasse.EjesAf);
            return View(vaerktoejskasse);
        }

        // POST: VaerktoejskassesApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VkasseId,Anskaffet,Fabrikat,EjesAf,Model,Serienummer,Farve")] Vaerktoejskasse vaerktoejskasse)
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
            ViewData["EjesAf"] = new SelectList(_context.Haandvaerker, "HaandvaerkerId", "Efternavn", vaerktoejskasse.EjesAf);
            return View(vaerktoejskasse);
        }

        // GET: VaerktoejskassesApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoejskasse = await _context.Vaerktoejskasse
                .Include(v => v.EjesAfNavigation)
                .FirstOrDefaultAsync(m => m.VkasseId == id);
            if (vaerktoejskasse == null)
            {
                return NotFound();
            }

            return View(vaerktoejskasse);
        }

        // POST: VaerktoejskassesApp/Delete/5
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
