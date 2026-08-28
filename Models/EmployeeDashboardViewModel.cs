using System.Collections.Generic;

namespace GiftoftheGivers.Models
{
    public class EmployeeDashboardViewModel
    {
        public int ActiveProjects { get; set; } = 12;
        public int VolunteerCount { get; set; }
        public string DonationsToday { get; set; } = "R42,500";
        public List<Volunteer> Volunteers { get; set; } = new();
        public List<ReliefUpdate> Updates { get; set; } = new();
        public string ActiveTab { get; set; } = "overview";
    }
}