using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salon.Data;
using Salon.Models;
using System.Diagnostics;

namespace Salon.Controllers
{
    public class ProduitSiteController : Controller
    {
        private readonly ILogger<ProduitSiteController> _logger;
        private readonly AppDbContext _context;

        public ProduitSiteController(ILogger<ProduitSiteController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Page About
        public IActionResult About()
        {
            return View("~/Views/Home/ProduitSite/about.cshtml");
        }

        // Blog Classic
        public IActionResult BlogClassic()
        {
            return View("~/Views/Home/ProduitSite/blog-classic.cshtml");
        }

        // Panier (Cart)
        public IActionResult Cart()
        {
            return View("~/Views/Home/ProduitSite/cart.cshtml");
        }
        

        // Checkout
        public IActionResult Checkout()
        {
            return View("~/Views/Home/ProduitSite/checkout.cshtml");
        }

        // Coming Soon
        public IActionResult ComingSoon()
        {
            return View("~/Views/Home/ProduitSite/coming-soon.cshtml");
        }

        // Contact
        public IActionResult Contact()
        {
            return View("~/Views/Home/ProduitSite/contact.cshtml");
        }

        // Error Page
        public IActionResult ErrorPage()
        {
            return View("~/Views/Home/ProduitSite/error-page.cshtml");
        }

        // FAQs
        public IActionResult FAQs()
        {
            return View("~/Views/Home/ProduitSite/faqs.cshtml");
        }

        // Home (index.cshtml)
        public IActionResult Index()
        {
            return View("~/Views/Home/ProduitSite/index.cshtml");
        }

        // My Account
        public IActionResult MyAccount()
        {
            return View("~/Views/Home/ProduitSite/my-account.cshtml");
        }

        // Order Tracking
        public IActionResult OrderTracking()
        {
            return View("~/Views/Home/ProduitSite/order-tracking.cshtml");
        }

        // Panier (cart alternative)
        public IActionResult Panier()
        {
            return View("~/Views/Home/ProduitSite/panier.cshtml");
        }

        // Shop Sidebar
        public IActionResult ShopSidebar()
        {
            return View("~/Views/Home/ProduitSite/shop-sidebar.cshtml");
        }

        // Single Post No Sidebar
        public IActionResult SinglePostNoSidebar()
        {
            return View("~/Views/Home/ProduitSite/single-post-no-sidebar.cshtml");
        }

        // Single Product
        public IActionResult SingleProduct()
        {
            return View("~/Views/Home/ProduitSite/single-product.cshtml");
        }

        // Wishlist
        public IActionResult Wishlist()
        {
            return View("~/Views/Home/ProduitSite/wishlist.cshtml");
        }

        // Gestion des erreurs
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
