namespace Salon.Models
{
    public class Fournisseur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }


        // Relations
        public ICollection<Produit> Produits { get; set; }
    }

}
