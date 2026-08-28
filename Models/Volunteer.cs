using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GiftoftheGivers.Models
{
    // Volunteer sign-up, captured via the public registration form and
    // listed on the Employee Dashboard.
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        // Comma-separated skill tags, e.g. "First Aid,Logistics"
        [Required(ErrorMessage = "Please select at least one skill.")]
        public string Skills { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select your availability.")]
        public string Availability { get; set; } = string.Empty;

        public string? Notes { get; set; }

        public DateTime DateRegistered { get; set; } = DateTime.Now;

        public string FullName => $"{FirstName} {LastName}";

        public string Initials => $"{(FirstName.Length > 0 ? FirstName[0] : ' ')}{(LastName.Length > 0 ? LastName[0] : ' ')}".ToUpper();

        public List<string> SkillList =>
            Skills.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

        public string AvailabilityLabel => Availability switch
        {
            "weekdays" => "Weekdays only",
            "weekends" => "Weekends only",
            "both" => "Weekdays & Weekends",
            "emergency" => "Emergency deployment (any time)",
            _ => Availability
        };
    }
}