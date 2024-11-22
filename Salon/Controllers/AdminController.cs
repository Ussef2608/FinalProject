using Salon.Data;
using Salon.Models;
using Salon.ViewModel;
using Microsoft.AspNetCore.Mvc;
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
    }
}
