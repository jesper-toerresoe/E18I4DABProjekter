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
    public class HaandvaerkerAppController : Controller
    {
        private readonly CraftManDBContext _context;

        public HaandvaerkerAppController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: HaandvaerkerApp
        public async Task<IActionResult> Index()
        {
            return View(await _context.Haandvaerker.ToListAsync());
        }

        // GET: HaandvaerkerApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haandvaerker = await _context.Haandvaerker
                .FirstOrDefaultAsync(m => m.HaandvaerkerId == id);
            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // GET: HaandvaerkerApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HaandvaerkerApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaandvaerkerId,Ansaettelsedato,Efternavn,Fagomraade,Fornavn")] Haandvaerker haandvaerker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(haandvaerker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: HaandvaerkerApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            if (haandvaerker == null)
            {
                return NotFound();
            }
            return View(haandvaerker);
        }

        // POST: HaandvaerkerApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaandvaerkerId,Ansaettelsedato,Efternavn,Fagomraade,Fornavn")] Haandvaerker haandvaerker)
        {
            if (id != haandvaerker.HaandvaerkerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haandvaerker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaandvaerkerExists(haandvaerker.HaandvaerkerId))
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
            return View(haandvaerker);
        }

        // GET: HaandvaerkerApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haandvaerker = await _context.Haandvaerker
                .FirstOrDefaultAsync(m => m.HaandvaerkerId == id);
            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // POST: HaandvaerkerApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var haandvaerker = await _context.Haandvaerker.FindAsync(id);
            _context.Haandvaerker.Remove(haandvaerker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HaandvaerkerExists(int id)
        {
            return _context.Haandvaerker.Any(e => e.HaandvaerkerId == id);
        }
    }
}
