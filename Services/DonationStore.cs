using System.Collections.Concurrent;
using GiftoftheGivers.Models;

namespace GiftoftheGivers.Services
{
    // In-memory dummy storage for the prototype stage, per the assignment brief.
    public class DonationStore
    {
        private readonly ConcurrentDictionary<int, Donation> _donations = new();
        private int _nextId = 0;

        public Donation Add(Donation donation)
        {
            donation.Id = Interlocked.Increment(ref _nextId);
            donation.ReferenceNumber = $"GOG-{DateTime.Now:yyyy}-{donation.Id:D5}";
            _donations[donation.Id] = donation;
            return donation;
        }

        public Donation? Get(int id) => _donations.TryGetValue(id, out var d) ? d : null;

        public IEnumerable<Donation> GetAll() => _donations.Values.OrderByDescending(d => d.DateSubmitted);

        public IEnumerable<Donation> GetForUser(string userId) =>
            GetAll().Where(d => d.OwnerUserId == userId);

        public int Count => _donations.Count;

        public decimal TotalZarEquivalent =>
            _donations.Values.Sum(d => d.Amount); // symbolic total for prototype metrics, currencies not converted
    }
}
