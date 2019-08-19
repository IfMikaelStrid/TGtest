using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.Models
{
    public class CoreTextDatabaseContext : DbContext
    {
        public CoreTextDatabaseContext(DbContextOptions<CoreTextDatabaseContext> options): base(options)
        {
        }

        public DbSet<Guestbook> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public class Guestbook
        {
            [Key]
            public int BlogId { get; set; }
            public string Url { get; set; }

            public ICollection<Post> Posts { get; set; }
        }

        public class Post
        {
            [Key]
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public int BlogId { get; set; }
            public Guestbook Guestbook { get; set; }
        }
    }
}

