using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models;
using Salon.ViewModel;

namespace Salon.Controllers
{
    public class ProduitController : Controller
    {
        private readonly AppDbContext _context;

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


    }
}

