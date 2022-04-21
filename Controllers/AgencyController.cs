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
    public class AgencyController : Controller
    {
        private readonly IAgencyService _agencyService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AgencyController(IAgencyService agencyService, IWebHostEnvironment host)
        {
            _agencyService = agencyService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAgencyRequestModel model)
        {

            var agency = await _agencyService.Create(model);
            if (agency.Status == true)
            {
                return RedirectToAction("SuperBoard", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var agencies = await _agencyService.GetAll();
            if(agencies.Status == true)
            {
                return View(agencies.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var agency = await _agencyService.Get(id);
            if (agency.Status == true)
            {
                return View(agency.Data);
            }
            return View();
        }
    }
}
