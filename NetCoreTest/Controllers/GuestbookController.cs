using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.Models;

namespace NetCoreTest.Controllers
{
    public class GuestbookController : Controller
    {
        [HttpPost]
        public IActionResult Index(string Title, string Content)
        {
            //var date = DateTime.Now;
            //using (var ctx = new CoreTextDatabaseContext())
            //{
            //    var post = new CoreTextDatabaseContext.Post() {
            //        Author = "",
            //        Title = Title,
            //        Content = Content,
            //        PublishTimeStamp = date
            //    };

            //    ctx.Posts.Add(post);
            //    ctx.SaveChanges();
            //}

            return View();
        }
    }
}