using NetCoreTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.Data
{
    public class DbInitializer
    {
        public static void Initialize(CoreTextDatabaseContext context)
        {
            context.Database.EnsureCreated();

            var posts = new Post[]
{
                new Post{Author="Mikael Strid", Content="First!",GuestBookId=1,PostId = new Guid().ToString(),PublishTimeStamp = DateTime.Now,Title="First"}
};

            foreach (Post c in posts)
            {
                context.Posts.Add(c);
            }
            // Look for any students.
            if (context.Guestbooks.Any())
            {
                return;   // DB has been seeded
            }

            var GuestBooks = new GuestBook[]
            {
            new GuestBook{GuestBookId=1,Posts = posts, Url=""},
            };
            foreach (GuestBook s in GuestBooks)
            {
                context.Guestbooks.Add(s);
            }
            context.SaveChanges();


            context.SaveChanges();
        }
    }
}
