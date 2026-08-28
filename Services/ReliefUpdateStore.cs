using System.Collections.Concurrent;
using GiftoftheGivers.Models;

namespace GiftoftheGivers.Services
{
    // In-memory dummy storage for Relief Pulse / emergency updates.
    public class ReliefUpdateStore
    {
        private readonly ConcurrentDictionary<int, ReliefUpdate> _updates = new();
        private int _nextId = 0;
        private bool _seeded = false;
        private readonly object _seedLock = new();

        public ReliefUpdate Add(ReliefUpdate update)
        {
            update.Id = Interlocked.Increment(ref _nextId);
            _updates[update.Id] = update;
            return update;
        }

        public IEnumerable<ReliefUpdate> GetAll() => _updates.Values.OrderByDescending(u => u.DatePosted);

        public int Count => _updates.Count;

        public void SeedIfEmpty()
        {
            if (_seeded) return;
            lock (_seedLock)
            {
                if (_seeded) return;
                Add(new ReliefUpdate { Title = "Emergency water tankers deployed", Project = "Flood Relief", Content = "Three water tankers have been dispatched to the Eastern Cape flood-affected areas. Distribution points are being set up at community centres in Mdantsane and King Williams Town.", Priority = "urgent", PostedBy = "Employee", DatePosted = new DateTime(2026, 8, 25) });
                Add(new ReliefUpdate { Title = "Food parcels distribution complete", Project = "Food Distribution", Content = "2,400 food parcels distributed across 12 community centres in KwaZulu-Natal over the weekend. Teams report all supplies delivered without incident.", Priority = "info", PostedBy = "Employee", DatePosted = new DateTime(2026, 8, 23) });
                Add(new ReliefUpdate { Title = "Volunteer briefing scheduled", Project = "Community Support", Content = "All newly registered volunteers for the Western Cape project: briefing session on Thursday 28 August at 09:00. Location will be shared via email.", Priority = "info", PostedBy = "Employee", DatePosted = new DateTime(2026, 8, 22) });
                _seeded = true;
            }
        }
    }
}
