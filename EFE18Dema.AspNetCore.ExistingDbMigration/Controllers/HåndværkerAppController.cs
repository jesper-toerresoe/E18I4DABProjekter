using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFE18Demo.AspNetCore.ExistingDbMigration.Models;

namespace EFE18Demo.AspNetCore.ExistingDbMigration.Controllers
{
    public class HåndværkerAppController : Controller
    {
        private readonly CraftManDBContext _context;

        public HåndværkerAppController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: HåndværkerApp
        public async Task<IActionResult> Index()
        {
            return View(await _context.Håndværker.ToListAsync());
        }

        // GET: HåndværkerApp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var håndværker = await _context.Håndværker
                .FirstOrDefaultAsync(m => m.HåndværkerId == id);
            if (håndværker == null)
            {
                return NotFound();
            }

            return View(håndværker);
        }

        // GET: HåndværkerApp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HåndværkerApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HåndværkerId,Ansættelsedato,Efternavn,Fagområde,Fornavn")] Håndværker håndværker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(håndværker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(håndværker);
        }

        // GET: HåndværkerApp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var håndværker = await _context.Håndværker.FindAsync(id);
            if (håndværker == null)
            {
                return NotFound();
            }
            return View(håndværker);
        }

        // POST: HåndværkerApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HåndværkerId,Ansættelsedato,Efternavn,Fagområde,Fornavn")] Håndværker håndværker)
        {
            if (id != håndværker.HåndværkerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(håndværker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HåndværkerExists(håndværker.HåndværkerId))
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
            return View(håndværker);
        }

        // GET: HåndværkerApp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var håndværker = await _context.Håndværker
                .FirstOrDefaultAsync(m => m.HåndværkerId == id);
            if (håndværker == null)
            {
                return NotFound();
            }

            return View(håndværker);
        }

        // POST: HåndværkerApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var håndværker = await _context.Håndværker.FindAsync(id);
            _context.Håndværker.Remove(håndværker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HåndværkerExists(int id)
        {
            return _context.Håndværker.Any(e => e.HåndværkerId == id);
        }
    }
}
