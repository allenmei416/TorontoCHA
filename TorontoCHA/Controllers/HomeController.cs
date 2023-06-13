using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TorontoCHA.Models;
using TorontoCHA.Entity;
using Microsoft.AspNetCore.Authorization;

namespace TorontoCHA.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

       
        public IActionResult Index(TchaAccount tchaAccount)
        {
            return View(tchaAccount);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View("~/Views/Home/Index.cshtml");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}