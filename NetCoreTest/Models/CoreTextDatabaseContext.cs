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

        public DbSet<GuestBook> Guestbooks { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestBook>().ToTable("GuestBook");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Upvote>().ToTable("Upvote");
        }

    }
}

