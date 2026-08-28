using System.Linq;
using GiftoftheGivers.Models;
using GiftoftheGivers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftoftheGivers.Controllers
{
    [Authorize(Roles = Roles.Donor)]
    public class DonorDashboardController : Controller
    {
        private readonly DonationStore _donationStore;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonorDashboardController(DonationStore donationStore, UserManager<ApplicationUser> userManager)
        {
            _donationStore = donationStore;
            _userManager = userManager;
        }

        // GET: /DonorDashboard
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var donations = user == null ? Enumerable.Empty<Donation>() : _donationStore.GetForUser(user.Id);
            return View(donations.ToList());
        }
    }
}
