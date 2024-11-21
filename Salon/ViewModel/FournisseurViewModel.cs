namespace Salon.ViewModel
{
    public class FournisseurViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }

        // Propriété pour afficher une liste des produits associés à ce fournisseur
        public List<ProduitViewModel> Produits { get; set; }
    }
}
