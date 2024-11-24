using System.ComponentModel.DataAnnotations.Schema;

namespace Salon.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        [Column(TypeName = "decimal(18, 2)")] // Définir la précision et l'échelle pour la colonne Prix
        public decimal Prix { get; set; }

        public string Description { get; set; }
        public string TypeDeSoins { get; set; }

        // Propriété pour le chemin de l'image
        public string ImagePath { get; set; }
    }
}
