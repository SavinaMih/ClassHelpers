using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassHelpers.Data;
using ClassHelpers.Models.Database;

namespace ClassHelpers.Controllers
{
    public class DatabasesController : Controller
    {
        private readonly ClassHelpersContext _context;

        public DatabasesController(ClassHelpersContext context)
        {
            _context = context;
        }

        // GET: Databases
        public async Task<IActionResult> Index()
        {
              return _context.Database != null ? 
                          View(await _context.Database.ToListAsync()) :
                          Problem("Entity set 'ClassHelpersContext.Database'  is null.");
        }

        // GET: Databases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Database == null)
            {
                return NotFound();
            }

            var database = await _context.Database
                .FirstOrDefaultAsync(m => m.Id == id);
            if (database == null)
            {
                return NotFound();
            }

            return View(database);
        }

        // GET: Databases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Databases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Link,Subject,Hint")] Database database)
        {
            if (ModelState.IsValid)
            {
                _context.Add(database);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(database);
        }

        // GET: Databases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Database == null)
            {
                return NotFound();
            }

            var database = await _context.Database.FindAsync(id);
            if (database == null)
            {
                return NotFound();
            }
            return View(database);
        }

        // POST: Databases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Link,Subject,Hint")] Database database)
        {
            if (id != database.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(database);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatabaseExists(database.Id))
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
            return View(database);
        }

        // GET: Databases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Database == null)
            {
                return NotFound();
            }

            var database = await _context.Database
                .FirstOrDefaultAsync(m => m.Id == id);
            if (database == null)
            {
                return NotFound();
            }

            return View(database);
        }

        // POST: Databases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Database == null)
            {
                return Problem("Entity set 'ClassHelpersContext.Database'  is null.");
            }
            var database = await _context.Database.FindAsync(id);
            if (database != null)
            {
                _context.Database.Remove(database);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatabaseExists(int id)
        {
          return (_context.Database?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
