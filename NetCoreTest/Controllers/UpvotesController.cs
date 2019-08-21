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
    public class UpvotesController : ControllerBase
    {
        private readonly CoreTextDatabaseContext _context;

        public UpvotesController(CoreTextDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Upvotes
        [HttpGet]
        public IEnumerable<Upvote> GetUpvotes()
        {
            return _context.Upvotes;
        }

        // GET: api/Upvotes/5
        [HttpGet("{id}")]
        public IActionResult GetUpvote([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var upvote = _context.Upvotes
                .Where(b => b.PostId == id);

            if (upvote == null)
            {
                return NotFound();
            }

            return Ok(upvote);
        }

        // PUT: api/Upvotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpvote([FromRoute] int id, [FromBody] Upvote upvote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != upvote.Id)
            {
                return BadRequest();
            }

            _context.Entry(upvote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpvoteExists(id))
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

        // POST: api/Upvotes
        [HttpPost]
        public async Task<IActionResult> PostUpvote([FromBody] Upvote upvote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var upvoteCheck = _context.Upvotes
                .Where(b => b.PostId == upvote.PostId && b.UserId == upvote.UserId);

            if (upvoteCheck.Any()) {
                return Conflict();
            }

            _context.Upvotes.Add(upvote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUpvote", new { id = upvote.Id }, upvote);
        }

        // DELETE: api/Upvotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUpvote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var upvote = await _context.Upvotes.FindAsync(id);
            if (upvote == null)
            {
                return NotFound();
            }

            _context.Upvotes.Remove(upvote);
            await _context.SaveChangesAsync();

            return Ok(upvote);
        }

        private bool UpvoteExists(int id)
        {
            return _context.Upvotes.Any(e => e.Id == id);
        }
    }
}