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
    public class VaerktoejAppController : Controller
    {
        private readonly CraftManDBContext _context;

        public VaerktoejAppController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: VaerktoejApp
        public async Task<IActionResult> Index()
        {
            var craftManDBContext = _context.Vaerktoej.Include(v => v.Vtk);
            return View(await craftManDBContext.ToListAsync());
        }

        // GET: VaerktoejApp/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej
                .Include(v => v.Vtk)
                .FirstOrDefaultAsync(m => m.VaerktoejsId == id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            return View(vaerktoej);
        }

        // GET: VaerktoejApp/Create
        public IActionResult Create()
        {
            ViewData["Vtkid"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId");
            return View();
        }

        // POST: VaerktoejApp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaerktoejsId,Vtanskaffet,Vtfabrikat,Vtmodel,Vtserienr,Vttype,Vtkid")] Vaerktoej vaerktoej)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaerktoej);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Vtkid"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId", vaerktoej.Vtkid);
            return View(vaerktoej);
        }

        // GET: VaerktoejApp/Edit/5
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
            ViewData["Vtkid"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId", vaerktoej.Vtkid);
            return View(vaerktoej);
        }

        // POST: VaerktoejApp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VaerktoejsId,Vtanskaffet,Vtfabrikat,Vtmodel,Vtserienr,Vttype,Vtkid")] Vaerktoej vaerktoej)
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
            ViewData["Vtkid"] = new SelectList(_context.Vaerktoejskasse, "VkasseId", "VkasseId", vaerktoej.Vtkid);
            return View(vaerktoej);
        }

        // GET: VaerktoejApp/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaerktoej = await _context.Vaerktoej
                .Include(v => v.Vtk)
                .FirstOrDefaultAsync(m => m.VaerktoejsId == id);
            if (vaerktoej == null)
            {
                return NotFound();
            }

            return View(vaerktoej);
        }

        // POST: VaerktoejApp/Delete/5
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
