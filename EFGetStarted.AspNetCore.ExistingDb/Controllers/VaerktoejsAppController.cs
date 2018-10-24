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
    public class VaerktoejsAppController : Controller
    {
        private readonly Database3Context _context;

        public VaerktoejsAppController(Database3Context context)
        {
            _context = context;
        }

        // GET: VaerktoejsApp
        public async Task<IActionResult> Index()
        {
            var database3Context = _context.Vaerktoej.Include(v => v.LiggerIvtkNavigation);
            return View(await database3Context.ToListAsync());
        }

        // GET: VaerktoejsApp/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej
                .Include(v => v.LiggerIvtkNavigation)
                .FirstOrDefaultAsync(m => m.VaerktoejsId == id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            return View(vaerktoej);
        }

        // GET: VaerktoejsApp/Create
        public IActionResult Create()
        {
            ViewData["LiggerIvtk"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId");
            return View();
        }

        // POST: VaerktoejsApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaerktoejsId,Anskaffet,Fabrikat,Model,Serienr,Type,LiggerIvtk")] Vaerktoej vaerktoej)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaerktoej);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LiggerIvtk"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId", vaerktoej.LiggerIvtk);
            return View(vaerktoej);
        }

        // GET: VaerktoejsApp/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            if (vaerktoej == null)
            {
                return NotFound();
            }
            ViewData["LiggerIvtk"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId", vaerktoej.LiggerIvtk);
            return View(vaerktoej);
        }

        // POST: VaerktoejsApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VaerktoejsId,Anskaffet,Fabrikat,Model,Serienr,Type,LiggerIvtk")] Vaerktoej vaerktoej)
        {
            if (id != vaerktoej.VaerktoejsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaerktoej);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaerktoejExists(vaerktoej.VaerktoejsId))
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
            ViewData["LiggerIvtk"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId", vaerktoej.LiggerIvtk);
            return View(vaerktoej);
        }

        // GET: VaerktoejsApp/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej
                .Include(v => v.LiggerIvtkNavigation)
                .FirstOrDefaultAsync(m => m.VaerktoejsId == id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            return View(vaerktoej);
        }

        // POST: VaerktoejsApp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var vaerktoej = await _context.Vaerktoej.FindAsync(id);
            _context.Vaerktoej.Remove(vaerktoej);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaerktoejExists(long id)
        {
            return _context.Vaerktoej.Any(e => e.VaerktoejsId == id);
        }
    }
}
