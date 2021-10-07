using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly Cadeteria cadeteria;
        private readonly Logger nlog;

        public HomeController(Logger nlog, Cadeteria cadeteria)
        {
            this.nlog = nlog;
            this.cadeteria = cadeteria;
        }

        public IActionResult Index()
        {
            return View(cadeteria);
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
