using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models;
using Salon.ViewModel;

namespace Salon.Controllers
{
    public class ServiceDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // Action pour afficher la liste des détails de service
        public IActionResult IndexServiceDetails()
        {
            // Récupérer les détails des services depuis la base de données, incluant les services associés
            var serviceDetailsList = _context.ServicesDétaillés
                .Include(sd => sd.Service) // Charger les données du service associé
                .Select(sd => new ServiceDetailsViewModel
                {
                    Id = sd.Id,
                    Name = sd.Name,
                    Prix = sd.Prix,
                    ServiceId = sd.ServiceId,
                    ServiceName = sd.Service.Nom // Ajouter le nom du service pour l'affichage
                })
                .ToList();

            // Retourner la vue avec la liste des données
            return View(serviceDetailsList);
        }

        // Action GET pour afficher le formulaire de création de service
        public IActionResult CreateServiceDetails()
        {
            // Récupérer la liste des services depuis la base de données
            ViewBag.Services = new SelectList(_context.Services, "Id", "Nom");

            // Retourner la vue avec un modèle vide pour remplir les champs
            return View(new ServiceDetailsViewModel());
        }

        // Action POST pour créer un nouveau service détaillé
        [HttpPost]
        public IActionResult CreateServiceDetails(ServiceDetailsViewModel serviceDetailsViewModel)
        {
            try
            {
                // Créer une nouvelle entité ServiceDetails à partir des données du ViewModel
                var serviceDetails = new ServiceDetails
                {
                    Name = serviceDetailsViewModel.Name,
                    Prix = serviceDetailsViewModel.Prix,
                    ServiceId = serviceDetailsViewModel.ServiceId
                };

                // Ajouter l'entité à la base de données
                _context.ServicesDétaillés.Add(serviceDetails);
                _context.SaveChanges();

                // Rediriger vers une autre action après la création
                return RedirectToAction(nameof(IndexServiceDetails));
            }
            catch (Exception ex)
            {
                // Log de l'exception si nécessaire (ex.Message)
                ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de l'ajout des détails du service.");

                // Réafficher la liste des services dans ViewBag pour la vue
                ViewBag.Services = new SelectList(_context.Services, "Id", "Nom", serviceDetailsViewModel.ServiceId);

                // Retourner la vue avec les données invalides pour correction
                return View(serviceDetailsViewModel);
            }
        }

        

        // Action pour supprimer un service détaillé
        public async Task<IActionResult> DeleteServiceDetails(int id)
        {
            var serviceDetail = await _context.ServicesDétaillés.FindAsync(id);

            if (serviceDetail != null)
            {
                // Supprimer le détail de service de la base de données
                _context.ServicesDétaillés.Remove(serviceDetail);
                await _context.SaveChangesAsync(); // Sauvegarder les modifications dans la base de données
            }

            // Redirection vers la page d'index des détails de service
            return RedirectToAction(nameof(IndexServiceDetails));
        }
    }
}
