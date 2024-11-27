using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Salon.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MyConnection")
    ));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddTransient<IEmailSender, clsEmailConfirm>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "about",
    pattern: "About",
    defaults: new { controller = "Home", action = "About" }
);
app.MapControllerRoute(
    name: "Service",
    pattern: "Service",
    defaults: new { controller = "Home", action = "Service" }
);
app.MapControllerRoute(
    name: "Reservation",
    pattern: "Reservation",
    defaults: new { controller = "Home", action = "Reservation" }
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Home", action = "Dashboard" });

app.MapControllerRoute(
    name: "ShopAbout",
    pattern: "Shop/About",
    defaults: new { controller = "ProduitSite", action = "About" });

app.MapControllerRoute(
    name: "ShopBlogClassic",
    pattern: "Shop/BlogClassic",
    defaults: new { controller = "ProduitSite", action = "BlogClassic" });

app.MapControllerRoute(
    name: "ShopCart",
    pattern: "Shop/Cart",
    defaults: new { controller = "ProduitSite", action = "Cart" });

app.MapControllerRoute(
    name: "ShopCheckout",
    pattern: "Shop/Checkout",
    defaults: new { controller = "ProduitSite", action = "Checkout" });

app.MapControllerRoute(
    name: "ShopComingSoon",
    pattern: "Shop/ComingSoon",
    defaults: new { controller = "ProduitSite", action = "ComingSoon" });

app.MapControllerRoute(
    name: "ShopContact",
    pattern: "Shop/Contact",
    defaults: new { controller = "ProduitSite", action = "Contact" });

app.MapControllerRoute(
    name: "ShopErrorPage",
    pattern: "Shop/ErrorPage",
    defaults: new { controller = "ProduitSite", action = "ErrorPage" });

app.MapControllerRoute(
    name: "ShopFAQs",
    pattern: "Shop/FAQs",
    defaults: new { controller = "ProduitSite", action = "FAQs" });

app.MapControllerRoute(
    name: "ShopIndex",
    pattern: "Shop",
    defaults: new { controller = "ProduitSite", action = "Index" });

app.MapControllerRoute(
    name: "ShopMyAccount",
    pattern: "Shop/MyAccount",
    defaults: new { controller = "ProduitSite", action = "MyAccount" });

app.MapControllerRoute(
    name: "ShopOrderTracking",
    pattern: "Shop/OrderTracking",
    defaults: new { controller = "ProduitSite", action = "OrderTracking" });

app.MapControllerRoute(
    name: "ShopPanier",
    pattern: "Shop/Panier",
    defaults: new { controller = "ProduitSite", action = "Panier" });

app.MapControllerRoute(
    name: "ShopShopSidebar",
    pattern: "Shop/ShopSidebar",
    defaults: new { controller = "ProduitSite", action = "ShopSidebar" });

app.MapControllerRoute(
    name: "ShopSinglePostNoSidebar",
    pattern: "Shop/SinglePostNoSidebar",
    defaults: new { controller = "ProduitSite", action = "SinglePostNoSidebar" });

app.MapControllerRoute(
    name: "ShopSingleProduct",
    pattern: "Shop/SingleProduct",
    defaults: new { controller = "ProduitSite", action = "SingleProduct" });

app.MapControllerRoute(
    name: "ShopWishlist",
    pattern: "Shop/Wishlist",
    defaults: new { controller = "ProduitSite", action = "Wishlist" });




app.UseEndpoints(endpoints => endpoints.MapRazorPages());
app.Run();
