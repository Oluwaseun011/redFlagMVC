using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using redFlag.Dtos;
using redFlag.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace redFlag.Controllers
{
    public class InitiatorController : Controller
    {
        private readonly IInitiatorService _initiatorService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InitiatorController(IInitiatorService initiatorService, IWebHostEnvironment host)
        {
            _initiatorService = initiatorService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateInitiatorRequestModel model)
        {
            var initiator = await _initiatorService.Create(model);
            if (initiator.Status == true)
            {
                 return RedirectToAction("Login", "User");
            }
            return View();
        }

        public IActionResult SubRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubRegister(MiniCreateInitiatorRequestModel model)
        {
            var initiator = await _initiatorService.MiniCreate(model);
            if (initiator.Status == true)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}
