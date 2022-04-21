using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using redFlag.Dtos;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace redFlag.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserService userService, IWebHostEnvironment host)
        {
            _userService = userService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
        {
            var user = await _userService.Login(model);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier , user.Data.Id.ToString()),
                     new Claim(ClaimTypes.Email, user.Data.Email),
                     new Claim(ClaimTypes.Name, user.Data.FirstName + " " + user.Data.LastName),
                     new Claim("Image", user.Data.Image),

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                if (user.Status == true)
                {
                    if (user.Data.Roles.Select(r => r.Name).Contains("SuperAdmin"))
                        return RedirectToAction("SuperBoard");

                    else if (user.Data.Roles.Select(r => r.Name).Contains("Initiator"))
                        return RedirectToAction("InitiatorBoard");

                    else if ((user.Data.Roles.Select(r => r.Name).Contains("AgencyAdmin")) 
                        && (user.Data.Roles.Select(r => r.Name).Contains("Staff")))
                        if(user.Data.AgencyAbbreviation == "NSEF")
                            return RedirectToAction("SecurityBoard");
                        else if (user.Data.AgencyAbbreviation == "NFES")
                            return RedirectToAction("FireBoard");
                        else if (user.Data.AgencyAbbreviation == "NEHC")
                            return RedirectToAction("HealthBoard");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult SuperBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InitiatorBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FireBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult HealthBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SecurityBoard()
        {
            return View();
        }
    }
}
