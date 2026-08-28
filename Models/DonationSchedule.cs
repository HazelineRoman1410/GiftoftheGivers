using System;
using System.Collections.Generic;
namespace GiftOfTheGivers.Models
{
    public class DonationSchedule
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public string Frequency { get; set; } = string.Empty; // Weekly, Monthly, Quarterly
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime NextRunDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Paused, Cancelled
        // Navigation
        public User User { get; set; } = null!;
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}