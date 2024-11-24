using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models;
using Salon.ViewModel;
using System.IO;

namespace Salon.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServiceController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> CreateService(ServiceViewModel serviceViewModel)
        {
        

            // Chemin où l'image sera stockée
            string imagePath = null;

            if (serviceViewModel.ImageFile != null)
            {
                // Créez un répertoire pour les images si nécessaire
                string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Générer un nom unique pour l'image
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + serviceViewModel.ImageFile.FileName;

                // Combinez le chemin du répertoire avec le nom du fichier
                imagePath = Path.Combine(uploadDir, uniqueFileName);

                // Sauvegarder le fichier
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await serviceViewModel.ImageFile.CopyToAsync(fileStream);
                }

                // Stocker le chemin relatif pour l'affichage
                imagePath = $"/images/services/{uniqueFileName}";
            }

            var service = new Service
            {
                Nom = serviceViewModel.Nom,
                Prix = serviceViewModel.Prix,
                Description = serviceViewModel.Description,
                TypeDeSoins = serviceViewModel.TypeDeSoins,
                ImagePath = imagePath // Enregistrer le chemin de l'image
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(IndexService));
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
                TypeDeSoins = service.TypeDeSoins,
                ImagePath = service.ImagePath // Afficher l'image actuelle
            };

            return View("~/Views/Service/EditService.cshtml", serviceViewModel); // Vue modification
        }

        [HttpPost]
        public async Task<IActionResult> EditService(ServiceViewModel serviceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceViewModel);
            }

            var service = await _context.Services.FindAsync(serviceViewModel.Id);
            if (service == null)
            {
                return NotFound();
            }

            // Mise à jour des champs du service
            service.Nom = serviceViewModel.Nom;
            service.Prix = serviceViewModel.Prix;
            service.Description = serviceViewModel.Description;
            service.TypeDeSoins = serviceViewModel.TypeDeSoins;

            // Gestion de l'image
            if (serviceViewModel.ImageFile != null)
            {
                var fileExtension = Path.GetExtension(serviceViewModel.ImageFile.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "Seules les images JPG et PNG sont autorisées.");
                    return View(serviceViewModel);
                }

                // Supprimez l'ancienne image si nécessaire
                if (!string.IsNullOrEmpty(service.ImagePath))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services", service.ImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Enregistrez la nouvelle image
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/services", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await serviceViewModel.ImageFile.CopyToAsync(stream);
                }

                service.ImagePath = uniqueFileName;
            }

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
                // Supprimez l'image associée si elle existe
                if (!string.IsNullOrEmpty(service.ImagePath))
                {
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, service.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(IndexService)); // Redirection vers l'index
        }
    }
}
