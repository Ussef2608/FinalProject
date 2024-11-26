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



app.UseEndpoints(endpoints => endpoints.MapRazorPages());
app.Run();
