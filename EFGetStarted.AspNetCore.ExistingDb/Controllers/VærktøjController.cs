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
    public class VærktøjController : Controller
    {
        private readonly CraftManDBContext _context;

        public VærktøjController(CraftManDBContext context)
        {
            _context = context;
        }

        // GET: Værktøj
        public async Task<IActionResult> Index()
        {
            var craftManDBContext = _context.Værktøj.Include(v => v.LiggerIvtkNavigation);
            return View(await craftManDBContext.ToListAsync());
        }

        // GET: Værktøj/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var værktøj = await _context.Værktøj
                .Include(v => v.LiggerIvtkNavigation)
                .FirstOrDefaultAsync(m => m.VærktøjsId == id);
            if (værktøj == null)
            {
                return NotFound();
            }

            return View(værktøj);
        }

        // GET: Værktøj/Create
        public IActionResult Create()
        {
            ViewData["LiggerIvtk"] = new SelectList(_context.Værktøjskasse, "VkasseId", "VkasseId");
            return View();
        }

        // POST: Værktøj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VærktøjsId,Anskaffet,Fabrikat,Model,Serienr,Type,LiggerIvtk")] Værktøj værktøj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(værktøj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LiggerIvtk"] = new SelectList(_context.Værktøjskasse, "VkasseId", "VkasseId", værktøj.LiggerIvtk);
            return View(værktøj);
        }

        // GET: Værktøj/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var værktøj = await _context.Værktøj.FindAsync(id);
            if (værktøj == null)
            {
                return NotFound();
            }
            ViewData["LiggerIvtk"] = new SelectList(_context.Værktøjskasse, "VkasseId", "VkasseId", værktøj.LiggerIvtk);
            return View(værktøj);
        }

        // POST: Værktøj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("VærktøjsId,Anskaffet,Fabrikat,Model,Serienr,Type,LiggerIvtk")] Værktøj værktøj)
        {
            if (id != værktøj.VærktøjsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(værktøj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VærktøjExists(værktøj.VærktøjsId))
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
            ViewData["LiggerIvtk"] = new SelectList(_context.Værktøjskasse, "VkasseId", "VkasseId", værktøj.LiggerIvtk);
            return View(værktøj);
        }

        // GET: Værktøj/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var værktøj = await _context.Værktøj
                .Include(v => v.LiggerIvtkNavigation)
                .FirstOrDefaultAsync(m => m.VærktøjsId == id);
            if (værktøj == null)
            {
                return NotFound();
            }

            return View(værktøj);
        }

        // POST: Værktøj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var værktøj = await _context.Værktøj.FindAsync(id);
            _context.Værktøj.Remove(værktøj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VærktøjExists(long id)
        {
            return _context.Værktøj.Any(e => e.VærktøjsId == id);
        }
    }
}
