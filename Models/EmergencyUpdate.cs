using System;
namespace GiftoftheGivers.Models
{
    public class EmergencyUpdate
    {
        public int UpdateId { get; set; }
        public int? ProjectId { get; set; }        // null when a general update
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Severity { get; set; } = "Medium"; // Low, Medium, High, Critical
        public int PostedByUserId { get; set; }
        public DateTime PostedAt { get; set; }

        // Navigation
        public ReliefProject? Project { get; set; }
        public User PostedByUser { get; set; } = null!;
    }
}