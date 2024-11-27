using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon.Data;
using Salon.Models; // Assurez-vous que ce namespace correspond à l'emplacement de votre modèle
using Salon.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Salon.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _dbContext;

        // Injecter le contexte de la base de données via le constructeur
        public ReservationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // READ : Liste des réservations
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reservations = await _dbContext.Reservations.ToListAsync();
            return View(reservations);
        }

        // CREATE : Afficher le formulaire de création
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ReservationViewModel());
        }

        // CREATE : Enregistrer une nouvelle réservation
        [HttpPost]
        public async Task<IActionResult> Create(ReservationViewModel model)

        {
            // Validation et formatage de la date
            string formattedDate = model.Date != default(DateTime)
                ? model.Date.ToString("yyyy-MM-dd") // Formatage ISO pour les dates
                : throw new ArgumentException("Invalid Date");

            // Validation et formatage de l'heure
            string formattedSchedule = model.Schedule != default(TimeSpan)
                ? DateTime.Today.Add(model.Schedule).ToString("hh:mm tt") // Format 12h avec AM/PM
                : throw new ArgumentException("Invalid Schedule");

            // Création de l'entité Reservation
            try
            {
                // Logique pour sauvegarder la réservation
                var reservation = new Reservation
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Service = model.Service,
                    Date = model.Date.ToString("yyyy-MM-dd"),
                    Schedule = DateTime.Today.Add(model.Schedule).ToString("hh:mm tt")
                };

                _dbContext.Reservations.Add(reservation);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "La réservation a été effectuée avec succès.";
                return Redirect("/Reservation#Reservation");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite. Veuillez réessayer.";
                return Redirect("/Reservation#Reservation");
            }

        }

        // READ : Détails d'une réservation spécifique
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // UPDATE : Afficher le formulaire de mise à jour
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var reservation = await _dbContext.Reservations.FindAsync(id);
        //    if (reservation == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new ReservationViewModel
        //    {
        //        Name = reservation.Name,
        //        Email = reservation.Email,
        //        PhoneNumber = reservation.PhoneNumber,
        //        Service = reservation.Service,
        //        Date = reservation.Date,
        //        Schedule = reservation.Schedule
        //    };

        //    return View(model);
        //}

        // UPDATE : Enregistrer les modifications d'une réservation
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, ReservationViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var reservation = await _dbContext.Reservations.FindAsync(id);
        //        if (reservation == null)
        //        {
        //            return NotFound();
        //        }

        //        reservation.Name = model.Name;
        //        reservation.Email = model.Email;
        //        reservation.PhoneNumber = model.PhoneNumber;
        //        reservation.Service = model.Service;
        //        reservation.Date = model.Date;
        //        reservation.Schedule = model.Schedule;

        //        _dbContext.Reservations.Update(reservation);
        //        await _dbContext.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }

        //    return View(model);
        //}

        // DELETE : Afficher la confirmation de suppression
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // DELETE : Supprimer une réservation
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
