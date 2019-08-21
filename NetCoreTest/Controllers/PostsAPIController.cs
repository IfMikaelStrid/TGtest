using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreTest.Models;


namespace NetCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsAPIController : ControllerBase
    {
        private readonly CoreTextDatabaseContext _context;
        private readonly AccountController _accountController;

        public PostsAPIController(CoreTextDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PostsAPI
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _context.Posts;
        }

        // GET: api/PostsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/PostsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] string id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/PostsAPI
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Identity.IsAuthenticated)
            {
                var GitHubName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;

                var user = _context.Users
                    .Where(b => b.UserName == GitHubName)
                    .FirstOrDefault();

                post.Author = GitHubName;
                post.AuthorId = user.UserId;
                post.PostId = Guid.NewGuid().ToString();
                post.PublishTimeStamp = DateTime.Now;
                post.GuestBookId = 1;
            }
            else {
                return Unauthorized();
            }

            _context.Posts.Add(post);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostExists(post.PostId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/PostsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(string id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}