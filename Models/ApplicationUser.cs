using Microsoft.AspNetCore.Identity;

namespace GiftoftheGivers.Models
{
    // Extends the default Identity user with the display-name fields
    // used across the app's nav/dashboard ("Welcome, First Last").
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
