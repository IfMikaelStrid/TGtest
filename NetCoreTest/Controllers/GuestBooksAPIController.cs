using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreTest.Models;

namespace NetCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestBooksAPIController : ControllerBase
    {
        private readonly CoreTextDatabaseContext _context;

        public GuestBooksAPIController(CoreTextDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/GuestBooksAPI
        [HttpGet]
        public IEnumerable<GuestBook> GetGuestbooks()
        {
            return _context.Guestbooks;
        }

        // GET: api/GuestBooksAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestBook = await _context.Guestbooks.FindAsync(id);

            if (guestBook == null)
            {
                return NotFound();
            }

            return Ok(guestBook);
        }

        // PUT: api/GuestBooksAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuestBook([FromRoute] int id, [FromBody] GuestBook guestBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guestBook.GuestBookId)
            {
                return BadRequest();
            }

            _context.Entry(guestBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GuestBooksAPI
        [HttpPost]
        public async Task<IActionResult> PostGuestBook([FromBody] GuestBook guestBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Guestbooks.Add(guestBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GuestBookExists(guestBook.GuestBookId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGuestBook", new { id = guestBook.GuestBookId }, guestBook);
        }

        // DELETE: api/GuestBooksAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var guestBook = await _context.Guestbooks.FindAsync(id);
            if (guestBook == null)
            {
                return NotFound();
            }

            _context.Guestbooks.Remove(guestBook);
            await _context.SaveChangesAsync();

            return Ok(guestBook);
        }

        private bool GuestBookExists(int id)
        {
            return _context.Guestbooks.Any(e => e.GuestBookId == id);
        }
    }
}