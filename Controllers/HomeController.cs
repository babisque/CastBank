using CastBank.Models;
using Core.Flash;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CastBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFlasher f;

        public HomeController(ILogger<HomeController> logger, IFlasher f)
        {
            this.f = f;
            _logger = logger;
        }

        public IActionResult Index()
        {
            f.Flash(Types.Success, "Flash message system for ASP.NET MVC Core", dismissable: true);
            
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
