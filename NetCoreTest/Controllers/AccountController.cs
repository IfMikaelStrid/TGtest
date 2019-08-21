using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace NetCoreTest.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private UserController _userController;
        
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            var challenge = Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
            return challenge;
        }

        [Authorize]
        public async Task Logout()
        {
            // Sign the user out of the cookie authentication middleware (i.e. it will clear the local session cookie)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Redirect("/");
        }

        [Authorize]
        public IActionResult UserProfile()
        {

            return View();
        }
    }
}