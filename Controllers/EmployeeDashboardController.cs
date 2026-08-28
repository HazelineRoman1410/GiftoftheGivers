using System.Linq;
using GiftoftheGivers.Models;
using GiftoftheGivers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftoftheGivers.Controllers
{
    [Authorize(Roles = Roles.Employee)]
    public class EmployeeDashboardController : Controller
    {
        private readonly ReliefUpdateStore _reliefUpdateStore;
        private readonly VolunteerStore _volunteerStore;
        private readonly DonationStore _donationStore;

        public EmployeeDashboardController(ReliefUpdateStore reliefUpdateStore, VolunteerStore volunteerStore, DonationStore donationStore)
        {
            _reliefUpdateStore = reliefUpdateStore;
            _volunteerStore = volunteerStore;
            _donationStore = donationStore;
        }

        // GET: /EmployeeDashboard?tab=overview
        public IActionResult Index(string tab = "overview")
        {
            _reliefUpdateStore.SeedIfEmpty();
            _volunteerStore.SeedIfEmpty();

            var model = new EmployeeDashboardViewModel
            {
                VolunteerCount = 344 + _volunteerStore.Count,
                DonationsToday = $"R{(42500 + _donationStore.Count * 500):N0}",
                Volunteers = _volunteerStore.GetAll().ToList(),
                Updates = _reliefUpdateStore.GetAll().ToList(),
                ActiveTab = tab
            };

            return View(model);
        }

        // POST: /EmployeeDashboard/PostUpdate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostUpdate(ReliefUpdate update)
        {
            if (!ModelState.IsValid)
            {
                var model = new EmployeeDashboardViewModel
                {
                    VolunteerCount = 344 + _volunteerStore.Count,
                    Volunteers = _volunteerStore.GetAll().ToList(),
                    Updates = _reliefUpdateStore.GetAll().ToList(),
                    ActiveTab = "post"
                };
                return View("Index", model);
            }

            update.PostedBy = User.Identity?.Name ?? "Employee";
            _reliefUpdateStore.Add(update);
            TempData["Toast"] = "Update published! (Logged via Azure Function to Azure Storage - simulated)";
            return RedirectToAction(nameof(Index), new { tab = "updates" });
        }
    }
}