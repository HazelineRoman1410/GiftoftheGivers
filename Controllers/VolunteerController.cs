using GiftoftheGivers.Models;
using GiftoftheGivers.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftoftheGivers.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly VolunteerStore _volunteerStore;

        public VolunteerController(VolunteerStore volunteerStore)
        {
            _volunteerStore = volunteerStore;
        }

        // GET: /Volunteer/Register
        public IActionResult Register()
        {
            return View(new Volunteer());
        }

        // POST: /Volunteer/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Volunteer volunteer, string[] skillCheckboxes)
        {
            volunteer.Skills = skillCheckboxes != null && skillCheckboxes.Length > 0
                ? string.Join(",", skillCheckboxes)
                : string.Empty;

            // The automatic [Required] check on Skills already ran against the
            // raw (unbound) form value before the line above fixed it up from
            // the checkboxes - clear that stale entry so it doesn't linger.
            ModelState.Remove(nameof(Volunteer.Skills));
            if (string.IsNullOrEmpty(volunteer.Skills))
            {
                ModelState.AddModelError(nameof(Volunteer.Skills), "Please select at least one skill.");
            }

            if (!ModelState.IsValid)
            {
                return View(volunteer);
            }

            _volunteerStore.Add(volunteer);
            TempData["VolunteerSuccess"] = true;
            return RedirectToAction(nameof(Register));
        }
    }
}
