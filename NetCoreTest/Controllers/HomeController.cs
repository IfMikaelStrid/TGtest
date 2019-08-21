﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.Models;

namespace NetCoreTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoreTextDatabaseContext _context;

        public HomeController(CoreTextDatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var ssoName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            var ssoEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            var _userController = new UserController(_context);

            var result = await _userController.GetUserByName(ssoName);

            var newUser = new Models.User()
            {
                UserEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                UserName = User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value,
                UserId = Guid.NewGuid().ToString(),
                UserImageURL = ""
            };
            await _userController.PostUser(newUser);
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
