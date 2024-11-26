using System.ComponentModel.DataAnnotations.Schema;

namespace Salon.Models
{
    public class ServiceDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")] // Définir la précision et l'échelle pour la colonne Prix
        public decimal Prix { get; set; }
        public int ServiceId { get; set; }

        // Relation avec Fournisseur
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}
