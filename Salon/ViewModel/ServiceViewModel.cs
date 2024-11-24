namespace Salon.ViewModel
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public string Description { get; set; }
        public string TypeDeSoins { get; set; }

        // Propriété pour afficher le chemin de l'image actuelle
        public string ImagePath { get; set; }

        // Propriété pour télécharger une nouvelle image
        public IFormFile ImageFile { get; set; }
    }
}
