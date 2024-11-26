using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salon.Data;
using Salon.Models;
using System.Diagnostics;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salon.Data;
using Salon.Models;


namespace Salon.Controllers
{
    public class ProduitSiteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public ProduitSiteController(ILogger<ProduitSiteController> logger, AppDbContext context)
        {
            _logger = (ILogger<HomeController>?)logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View("~/Views/Home/ProduitSite/Home.cshtml");
        }
        public IActionResult Produit()
        {
            return View("~/Views/Home/ProduitSite/Home.cshtml");
        }
        public IActionResult Blog()
        {
            return View("~/Views/Home/ProduitSite/Home.cshtml");
        }
        public IActionResult Contact()
        {
            return View("~/Views/Home/ProduitSite/Home.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
