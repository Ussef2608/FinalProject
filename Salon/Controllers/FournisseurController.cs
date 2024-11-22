using Salon.Data;
using Salon.Models;
using Salon.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meltier.Controllers
{
    public class FournisseurController : Controller
    {
        private readonly AppDbContext _context;

        public FournisseurController(AppDbContext context)
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
            
                try
                {
                    // Mise à jour dans la base de données
                    _context.Fournisseurs.Update(fournisseur);
                    await _context.SaveChangesAsync();

                    // Redirection vers IndexFournisseur si tout est OK
                    return RedirectToAction("IndexFournisseur", "Fournisseur");
                }
                catch (Exception ex)
                {
                    // En cas d'erreur inattendue, ajoutez un message d'erreur personnalisé
                    ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la mise à jour. Veuillez réessayer.");
                }

            // Si le modèle n'est pas valide ou qu'une exception survient, rester sur la vue actuelle
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
