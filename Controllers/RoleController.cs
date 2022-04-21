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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoleController(IRoleService roleService, IWebHostEnvironment host)
        {
            _roleService = roleService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {

            var role = await _roleService.Create(model);
            if (role.Status == true)
            {
                return RedirectToAction("SuperBoard", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var roles = await _roleService.GetAll();
            if (roles.Status == true)
            {
                return View(roles.Data);
            }
            return View();
        }
    }
}
