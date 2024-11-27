using System.ComponentModel.DataAnnotations;

namespace Salon.ViewModel
{
    public class ReservationViewModel
    {
        public int Id { get; set; } // Facultatif pour suivre les modifications des données

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Service { get; set; }

        [Required]
        public DateTime Date { get; set; } // La date sélectionnée dans le formulaire

        [Required]
        public TimeSpan Schedule { get; set; } // L'heure sélectionnée dans le formulaire
    }
}
