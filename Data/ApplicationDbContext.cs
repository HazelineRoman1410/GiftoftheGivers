using GiftoftheGivers.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftoftheGivers.Data
{
    // SQLite-backed Identity store. This is the "minimal persistence" the
    // prototype brief allows - swap for Azure SQL in the full production build.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
