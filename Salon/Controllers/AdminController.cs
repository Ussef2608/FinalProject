using Salon.Data;
using Salon.Models;
using Salon.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Meltier.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------
        // Actions CRUD pour Fournisseur
        // ----------------------------
        public async Task<IActionResult> IndexFournisseur()
        {
            var fournisseurs = await _context.Fournisseurs.ToListAsync();
            return View(fournisseurs);
        }



        public IActionResult CreateFournisseur()
        {

            var fournisseurVm = new FournisseurViewModel();
            return View(fournisseurVm);
        }


        // POST : CreateFournisseur

        [HttpPost]
        public IActionResult CreateFournisseur(FournisseurViewModel model)
        {

            // Créer une instance de Fournisseur avec les données du modèle
            var fournisseur = new Fournisseur
            {
                Id = model.Id,
                Nom = model.Nom,
                Contact = model.Contact,
                Email = model.Email,
                Adresse = model.Adresse,
                Ville = model.Ville,
                Pays = model.Pays
            };

            _context.Fournisseurs.Add(fournisseur);
            _context.SaveChanges();
            return RedirectToAction(nameof(IndexFournisseur));

        }

        public async Task<IActionResult> EditFournisseur(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null) return NotFound();
            return View(fournisseur);
        }

        [HttpPost]
        public async Task<IActionResult> EditFournisseur(Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                _context.Fournisseurs.Update(fournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexFournisseur));
            }
            return View(fournisseur);
        }

        public async Task<IActionResult> DeleteFournisseur(int id)
        {
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur != null)
            {
                _context.Fournisseurs.Remove(fournisseur);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(IndexFournisseur));
        }
        // ----------------------------
        // Actions CRUD pour Produit
        // ----------------------------
        public async Task<IActionResult> IndexProduit()
        {
            var produits = await _context.Produits
           .Include(p => p.Fournisseur) // Charge les données des fournisseurs
           .ToListAsync();
            return View(produits);
        }

        public IActionResult CreateProduit()
        {
            // Récupérer la liste des fournisseurs depuis la base de données
            ViewBag.Fournisseurs = new SelectList(_context.Fournisseurs, "Id", "Nom");

            // Retourner la vue avec un modèle vide pour remplir les champs
            return View(new ProduitViewModel());
        }

        [HttpPost]
        public IActionResult CreateProduit(ProduitViewModel produitViewModel)
        {



            try
            {
                var produit = new Produit
                {
                    Nom = produitViewModel.Nom,
                    Description = produitViewModel.Description,
                    Prix = produitViewModel.Prix,
                    StockDisponible = produitViewModel.StockDisponible,
                    DatePeremption = produitViewModel.DatePeremption,
                    FournisseurId = produitViewModel.FournisseurId
                };

                _context.Produits.Add(produit);
                _context.SaveChanges();

                return RedirectToAction(nameof(IndexProduit));
            }
            catch (Exception ex)
            {
                // Log the exception (ex.Message) if needed
                ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de l'ajout du produit.");
                ViewBag.Fournisseurs = new SelectList(_context.Fournisseurs, "Id", "Nom", produitViewModel.FournisseurId);
                return View(produitViewModel);
            }
        }



        public async Task<IActionResult> EditProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();
            return View(produit);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduit(Produit produit)
        {
            if (ModelState.IsValid)
            {
                _context.Produits.Update(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexProduit));
            }
            return View(produit);
        }

        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(IndexProduit));
        }

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
