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
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IAgencyService _agencyService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StaffController(IStaffService staffService, IAgencyService agencyService, IWebHostEnvironment host)
        {
            _staffService = staffService;
            _agencyService = agencyService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateStaffRequestModel model)
        {
            var staff = await _staffService.Create(model);
            if (staff.Status == true)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAgencyAdmin(int agencyId)
        {
            var agency = await _agencyService.Get(agencyId);
            return View(agency.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgencyAdmin(CreateAgencyAdminRequestModel model, int id)
        {
            var staff = await _staffService.CreateAgencyAdmin(model, id);
            if (staff.Status == true)
            {
                return RedirectToAction("SuperBoard", "User");
            }
            return View();
        }
    }
}
