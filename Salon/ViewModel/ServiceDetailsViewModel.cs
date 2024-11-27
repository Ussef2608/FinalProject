using System.ComponentModel.DataAnnotations;

namespace Salon.ViewModel
{
    public class ServiceDetailsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Display(Name = "Prix")]
        public decimal Prix { get; set; }

        [Required(ErrorMessage = "L'identifiant du service est obligatoire.")]
        [Display(Name = "Service associé")]
        public int ServiceId { get; set; }

        [Display(Name = "Nom du service")]
        public string ServiceName { get; set; } // Pour afficher le nom du service associé si nécessaire
    }
}
