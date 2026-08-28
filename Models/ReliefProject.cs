using System;
using System.Collections.Generic;
namespace GiftOfTheGivers.Models
{
    public class ReliefProject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "Planned"; // Planned, Active, Completed, Cancelled
        public int CreatedByUserId { get; set; }
        // Navigation
        public User CreatedByUser { get; set; } = null!;
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<EmergencyUpdate> EmergencyUpdates { get; set; } = new List<EmergencyUpdate>();
        public ICollection<VolunteerSignup> VolunteerSignups { get; set; } = new List<VolunteerSignup>();
    }
}