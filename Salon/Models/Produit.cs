using System.ComponentModel.DataAnnotations.Schema;

namespace Salon.Models
{
    public class Produit
    {
        public int IdProduit { get; set; }  // Id du produit
        public string Nom { get; set; }  // Nom du produit
        public string Description { get; set; }  // Description du produit

        [Column(TypeName = "decimal(18, 2)")]  // Définir la précision et l'échelle pour la colonne Prix
        public decimal Prix { get; set; }  // Prix du produit

        public int StockDisponible { get; set; }  // Quantité de stock disponible
        public DateTime DatePeremption { get; set; }  // Date de péremption du produit

        // Clé étrangère vers Fournisseur
        public int FournisseurId { get; set; }

        // Relation avec Fournisseur
        [ForeignKey("FournisseurId")]
        public Fournisseur Fournisseur { get; set; }
    }
}
