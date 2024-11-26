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
                imagePath =uniqueFileName;
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
           

            var service = await _context.Services.FindAsync(serviceViewModel.Id);
            if (service == null)
            {
                return NotFound();
            }

            // Variables pour les chemins d'images
            string oldImagePath = service.ImagePath;
            string newImagePath = oldImagePath; // Par défaut, la nouvelle image est la même que l'ancienne

            // Mise à jour des champs texte
            service.Nom = serviceViewModel.Nom;
            service.Prix = serviceViewModel.Prix;
            service.Description = serviceViewModel.Description;
            service.TypeDeSoins = serviceViewModel.TypeDeSoins;

            // Gestion de l'image
            if (serviceViewModel.ImageFile != null && serviceViewModel.ImageFile.Length > 0)
            {
                // Validation de l'extension de fichier
                var fileExtension = Path.GetExtension(serviceViewModel.ImageFile.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ImageFile", "Seules les images JPG et PNG sont autorisées.");
                    return View(serviceViewModel);
                }

                // Générer un nom unique pour le fichier
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var imagePath = Path.Combine("wwwroot/images/services", uniqueFileName);

                // Sauvegarder le fichier sur le serveur
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await serviceViewModel.ImageFile.CopyToAsync(stream);
                }

                // Mettre à jour le chemin de la nouvelle image
                newImagePath = uniqueFileName;

                // Supprimer l'ancienne image si elle existe
                if (!string.IsNullOrEmpty(service.ImagePath))
                {
                    var fullOldImagePath = Path.Combine("wwwroot/images/services", service.ImagePath);
                    if (System.IO.File.Exists(fullOldImagePath))
                    {
                        System.IO.File.Delete(fullOldImagePath);
                    }
                }

                // Mettre à jour le chemin dans le service
                service.ImagePath = uniqueFileName;
            }

            // Mise à jour du service dans la base de données
            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            // Retourner les chemins d'images
            return Json(new
            {
                Message = "Service mis à jour avec succès",
                OldImagePath = $"/images/services/{oldImagePath}",
                NewImagePath = $"/images/services/{newImagePath}"
            });
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
