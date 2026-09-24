using System;
namespace GiftoftheGivers.Models
{
    public class VolunteerSignup
    {
        public int SignupId { get; set; }
        public int VolunteerId { get; set; }
        public int ProjectId { get; set; }
        public DateTime SignupDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled
        // Navigation
        public Volunteer Volunteer { get; set; } = null!;
        public ReliefProject Project { get; set; } = null!;
    }
}