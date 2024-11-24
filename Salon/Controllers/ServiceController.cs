using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models;
using Salon.ViewModel;

namespace Salon.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------
        // Liste des services
        // ----------------------------
        public async Task<IActionResult> IndexService()
        {
            var services = await _context.Services.ToListAsync();
            return View("~/Views/Service/IndexService.cshtml", services); // Vue index
        }

        // ----------------------------
        // Formulaire de création
        // ----------------------------
        public IActionResult CreateService()
        {
            var service = new ServiceViewModel();
            return View("~/Views/Service/CreateService.cshtml", service); // Vue création
        }

        [HttpPost]
        public IActionResult CreateService(ServiceViewModel serviceViewModel)
        {
            
                var service = new Service
                {
                    Nom = serviceViewModel.Nom,
                    Prix = serviceViewModel.Prix,
                    Description = serviceViewModel.Description,
                    TypeDeSoins = serviceViewModel.TypeDeSoins
                };

                _context.Services.Add(service);
                _context.SaveChanges();
                return RedirectToAction(nameof(IndexService)); // Redirection vers l'index

        }

        // ----------------------------
        // Formulaire de modification
        // ----------------------------
        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound(); // Si le service n'existe pas
            }

            var serviceViewModel = new ServiceViewModel
            {
                Id = service.Id,
                Nom = service.Nom,
                Prix = service.Prix,
                Description = service.Description,
                TypeDeSoins = service.TypeDeSoins
            };

            return View("~/Views/Service/EditService.cshtml", serviceViewModel); // Vue modification
        }

        

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceViewModel serviceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Service/EditService.cshtml", serviceViewModel);
            }

            var service = await _context.Services.FindAsync(serviceViewModel.Id);
            if (service == null)
            {
                return NotFound();
            }

            service.Nom = serviceViewModel.Nom;
            service.Prix = serviceViewModel.Prix;
            service.Description = serviceViewModel.Description;
            service.TypeDeSoins = serviceViewModel.TypeDeSoins;

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(IndexService));
        }





        // ----------------------------
        // Suppression d'un service
        // ----------------------------
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(IndexService)); // Redirection vers l'index
        }
    }
}
