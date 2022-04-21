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
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IAgencyService _agencyService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BranchController(IBranchService branchService, IAgencyService agencyService, IWebHostEnvironment host)
        {
            _branchService = branchService;
            _agencyService = agencyService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public async Task<IActionResult> Create( int agencyId)
        {
            var agency = await _agencyService.Get(agencyId);
            return View(agency.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchRequestModel model, int id)
        {

            var branch = await _branchService.Create(model, id);
            if (branch.Status == true)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        public async Task<IActionResult> CreateHead(int agencyId)
        {
            var agency = await _agencyService.Get(agencyId);
            return View(agency.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHead(CreateHeadquatersRequestModel model, int id)
        {

            var branch = await _branchService.CreateHeadquaters(model, id);
            if (branch.Status == true)
            {
                return RedirectToAction("CreateAgencyAdmin", "Staff", new {id} );
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var branches = await _branchService.GetAll();
            if (branches.Status == true)
            {
                return View(branches.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var branch = await _branchService.Get(id);
            if (branch.Status == true)
            {
                return View(branch.Data);
            }
            return View();
        }
    }
}
