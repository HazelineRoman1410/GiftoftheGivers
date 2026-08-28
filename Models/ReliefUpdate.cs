using System;
using System.ComponentModel.DataAnnotations;

namespace GiftoftheGivers.Models
{
    // "Relief Pulse" - relief project / emergency updates posted by employees
    public class ReliefUpdate
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Update Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Related Project")]
        public string Project { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Update Content")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Priority Level")]
        public string Priority { get; set; } = "info"; // info, urgent, critical

        [Display(Name = "Posted By")]
        public string PostedBy { get; set; } = "Employee";

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public string PriorityIcon => Priority switch
        {
            "urgent" => "🔴",
            "critical" => "⚠️",
            _ => ""
        };
    }
}
