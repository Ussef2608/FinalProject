using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salon.Data;
using Salon.Models;
using Salon.ViewModel;

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
            // Récupérer tous les services
            var services = _context.Services.ToList();

            // Récupérer les 4 premiers détails de services, triés par un critère spécifique (par exemple, prix)
            var top4ServiceDetails = _context.ServicesDétaillés
                                             .OrderBy(sd => sd.Prix)  // Vous pouvez ajuster le critère de tri ici
                                             .Take(4)                 // Limite à 4 éléments
                                             .ToList();

            // Créer et remplir le ViewModel
            var viewModel = new HomeViewModel
            {
                Services = services,
                Top4ServiceDetails = top4ServiceDetails
            };

            return View(viewModel);  // Passez le ViewModel à la vue
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
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
