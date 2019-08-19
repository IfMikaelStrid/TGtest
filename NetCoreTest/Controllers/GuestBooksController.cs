using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreTest.Models;

namespace NetCoreTest.Controllers
{
    public class GuestBooksController : Controller
    {
        private readonly CoreTextDatabaseContext _context;

        public GuestBooksController(CoreTextDatabaseContext context)
        {
            _context = context;
        }

        // GET: GuestBooks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guestbooks.ToListAsync());
        }

        // GET: GuestBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.Guestbooks
                .FirstOrDefaultAsync(m => m.GuestBookId.Equals(id));
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // GET: GuestBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Url")] GuestBook guestBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestBook);
        }

        // GET: GuestBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.Guestbooks.FindAsync(id);
            if (guestBook == null)
            {
                return NotFound();
            }
            return View(guestBook);
        }

        // POST: GuestBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Url")] GuestBook guestBook)
        {
            if (id != guestBook.GuestBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookExists(guestBook.GuestBookId))
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
            return View(guestBook);
        }

        // GET: GuestBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBook = await _context.Guestbooks
                .FirstOrDefaultAsync(m => m.GuestBookId.Equals(id));
            if (guestBook == null)
            {
                return NotFound();
            }

            return View(guestBook);
        }

        // POST: GuestBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guestBook = await _context.Guestbooks.FindAsync(id);
            _context.Guestbooks.Remove(guestBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookExists(int id)
        {
            return _context.Guestbooks.Any(e => e.GuestBookId != id);
        }
    }
}
