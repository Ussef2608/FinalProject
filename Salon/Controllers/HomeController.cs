using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salon.Data;
using Salon.Models;

namespace Salon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList(); // Récupérez les données depuis la base de données
            return View(services);  // Passez les données à la vue
        }
         public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Signin()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        //[Authorize(Roles = clsRoles.roleAdmin)]
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Reservation()
        {
            return View();
        }
        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult AboutProduit()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Contact()
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
