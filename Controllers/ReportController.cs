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
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(IReportService reportService, IWebHostEnvironment host)
        {
            _reportService = reportService;
            _webHostEnvironment = host;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReportRequestModel model)
        {

            var report = await _reportService.Create(model);
            if (report.Status == true)
            {
                return RedirectToAction("InitiatorBoard", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MyReports(int id)
        {
            var report = await _reportService.GetSelected(id);
            if (report.Status == true)
            {
                return View(report.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CancelReport(int id)
        {
            var report = await _reportService.CancelReport(id);
            if (report.Status == true)
            {
                return RedirectToAction("MyReports");
            }
            return View();
        }

        

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var reports = await _reportService.GetAll();
            if (reports.Status == true)
            {
                return View(reports.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var report = await _reportService.Get(id);
            if (report.Status == true)
            {
                return View(report.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Enroute(int id)
        {
            var report = await _reportService.Enroute(id);
            if (report.Status == true)
            {
                return View(report.Data);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Arrived(int id)
        {
            var report = await _reportService.Arrived(id);
            if (report.Status == true)
            {
                return View(report.Data);
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Done(int id)
        {
            var report = await _reportService.Done(id);
            if (report.Status == true)
            {
                return RedirectToAction("SuperBoard", "User");
            }
            return View();
        }
    }
}
