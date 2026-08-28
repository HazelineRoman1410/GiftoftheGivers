using System.Diagnostics;
using GiftoftheGivers.Models;
using GiftoftheGivers.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftoftheGivers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VolunteerStore _volunteerStore;
        private readonly ReliefUpdateStore _reliefUpdateStore;
        private readonly DonationStore _donationStore;

        public HomeController(ILogger<HomeController> logger, VolunteerStore volunteerStore, ReliefUpdateStore reliefUpdateStore, DonationStore donationStore)
        {
            _logger = logger;
            _volunteerStore = volunteerStore;
            _reliefUpdateStore = reliefUpdateStore;
            _donationStore = donationStore;
        }

        public IActionResult Index()
        {
            _volunteerStore.SeedIfEmpty();
            _reliefUpdateStore.SeedIfEmpty();

            // Base figures mirror the original prototype's demo numbers,
            // plus whatever's been added live in this running session.
            ViewBag.ActiveProjects = 12;
            ViewBag.RegisteredVolunteers = 344 + _volunteerStore.Count;
            ViewBag.DonationProgress = "69%";

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(string name, string email, string message)
        {
            // Prototype stage: no email/ticketing backend yet - just acknowledge receipt.
            TempData["Toast"] = "Message sent! We'll get back to you within 24 hours.";
            return RedirectToAction(nameof(Contact));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
