using System;
using System.ComponentModel.DataAnnotations;

namespace GiftoftheGivers.Models
{
    // Prototype-stage donation record. Persistence is in-memory for now
    // (see Services/DonationStore.cs) - swap for Azure SQL in Part 2.
    public class Donation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name, or choose to donate anonymously.")]
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; } = "Anonymous";

        public string? DonorEmail { get; set; }

        public bool IsAnonymous { get; set; }

        [Required]
        [Range(1, 1000000, ErrorMessage = "Please enter an amount greater than 0.")]
        public decimal Amount { get; set; }

        [Required]
        public Currency Currency { get; set; } = Currency.ZAR;

        [Required]
        [Display(Name = "Donation Type")]
        public DonationType DonationType { get; set; } = DonationType.OnceOff;

        [Required]
        [Display(Name = "Allocate To")]
        public string Project { get; set; } = "General Relief Fund";

        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public string ReferenceNumber { get; set; } = string.Empty;

        // Links the donation to a logged-in Donor account (null for guest/anonymous donations)
        public string? OwnerUserId { get; set; }

        public string CurrencySymbol => Currency switch
        {
            Currency.USD => "$",
            Currency.EUR => "€",
            _ => "R"
        };

        public string FormattedAmount => $"{CurrencySymbol}{Amount:N2}";
    }
}