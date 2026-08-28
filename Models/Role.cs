using System.Collections.Generic;
namespace GiftOfTheGivers.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty; // "Donor", "Employee"
        // Navigation
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}