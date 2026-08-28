using System;
namespace GiftOfTheGivers.Models
{
    public class TaxCertificate
    {
        public int CertificateId { get; set; }
        public int DonationId { get; set; }        // 1:1 with Donation
        public string CertificateNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public int TaxYear { get; set; }
        public decimal Amount { get; set; }
        // Navigation
        public Donation Donation { get; set; } = null!;
    }
}