using Microsoft.AspNetCore.Mvc.Rendering;

namespace Salon.ViewModel
{
    public class ProduitViewModel
    {
        public int IdProduit { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public decimal Prix { get; set; }
        public int StockDisponible { get; set; }
        public DateTime DatePeremption { get; set; }

        // Fournisseur associé au produit
        public int FournisseurId { get; set; }

        // Liste des fournisseurs pour la sélection dans un dropdown
        public IEnumerable<SelectListItem> Fournisseurs { get; set; }


    }
}

