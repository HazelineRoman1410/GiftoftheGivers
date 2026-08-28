using GiftoftheGivers.Models;
using GiftoftheGivers.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftoftheGivers.Controllers
{
    public class DonationController : Controller
    {
        private readonly DonationStore _donationStore;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationController(DonationStore donationStore, UserManager<ApplicationUser> userManager)
        {
            _donationStore = donationStore;
            _userManager = userManager;
        }

        // GET: /Donation/Create
        public IActionResult Create()
        {
            return View(new Donation { Amount = 500 });
        }

        // POST: /Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation donation)
        {
            if (donation.IsAnonymous)
            {
                ModelState.Remove(nameof(Donation.DonorName));
                ModelState.Remove(nameof(Donation.DonorEmail));
                donation.DonorName = "Anonymous";
                donation.DonorEmail = null;
            }

            if (!ModelState.IsValid)
            {
                return View(donation);
            }

            // If a logged-in Donor is making this (non-anonymous) donation, link it
            // to their account so it shows up on their donor dashboard/history.
            if (!donation.IsAnonymous && User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, Roles.Donor))
                {
                    donation.OwnerUserId = user.Id;
                    if (string.IsNullOrWhiteSpace(donation.DonorName)) donation.DonorName = user.FullName;
                }
            }

            var saved = _donationStore.Add(donation);
            return RedirectToAction(nameof(Confirmation), new { id = saved.Id });
        }

        // GET: /Donation/Confirmation/5
        public IActionResult Confirmation(int id)
        {
            var donation = _donationStore.Get(id);
            if (donation == null) return NotFound();
            return View(donation);
        }
    }
}
