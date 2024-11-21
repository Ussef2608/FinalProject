using Salon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Salon.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "426fb1ce-9a43-41e0-b13d-71ada8b39f77", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "76ede889-ef5b-44ce-9e72-7217e61328d8", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "3c46d0ae-0d90-40e0-a9d5-8b380f124caf", Name = "LimitedUser", NormalizedName = "LIMITEDUSER" }
);

            // Définition explicite des clés primaires
            modelBuilder.Entity<Fournisseur>()
                .HasKey(f => f.Id);  // Clé primaire pour Fournisseur

            modelBuilder.Entity<Produit>()
                .HasKey(p => p.IdProduit);  // Clé primaire pour Produit

            modelBuilder.Entity<Service>()
                .HasKey(s => s.Id);  // Clé primaire pour Service

            // Configuration des relations et autres contraintes
            modelBuilder.Entity<Produit>()
                .HasOne(p => p.Fournisseur)
                .WithMany(f => f.Produits)
                .HasForeignKey(p => p.FournisseurId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fournisseur>()
                .HasIndex(f => f.Nom)
                .IsUnique();


        }
    }

}