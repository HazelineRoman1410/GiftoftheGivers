using System;
using System.Collections.Generic;
namespace GiftoftheGivers.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        // Navigation
        public Role Role { get; set; } = null!;
        public Volunteer? Volunteer { get; set; }                              // 1:1 optional
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<DonationSchedule> DonationSchedules { get; set; } = new List<DonationSchedule>();
        public ICollection<ReliefProject> ProjectsCreated { get; set; } = new List<ReliefProject>();
        public ICollection<EmergencyUpdate> EmergencyUpdatesPosted { get; set; } = new List<EmergencyUpdate>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}