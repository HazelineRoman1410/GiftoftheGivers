using System;
namespace GiftoftheGivers.Models
{
    public class AuditLog
    {
        public int LogId { get; set; }
        public int? UserId { get; set; }           // null for system-generated actions
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
        // Navigation
        public User? User { get; set; }
    }
}