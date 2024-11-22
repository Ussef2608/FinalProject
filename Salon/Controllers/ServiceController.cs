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
        // ----------------------------
        // Actions CRUD pour Service
        // ----------------------------
        public async Task<IActionResult> IndexService()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        public IActionResult CreateService()
        {
            var service = new ServiceViewModel();
            return View(service);
        }

        [HttpPost]
        public IActionResult CreateService(ServiceViewModel serviceViewModel)
        {
            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(IndexService));
            }

            return View(serviceViewModel);
        }


        public async Task<IActionResult> EditService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexService));
            }
            return View(service);
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(IndexService));
        }
    }
}
