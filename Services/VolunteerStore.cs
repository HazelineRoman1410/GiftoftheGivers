using System.Collections.Concurrent;
using GiftoftheGivers.Models;

namespace GiftoftheGivers.Services
{
    // In-memory dummy storage for volunteer sign-ups (prototype stage).
    public class VolunteerStore
    {
        private readonly ConcurrentDictionary<int, Volunteer> _volunteers = new();
        private int _nextId = 0;
        private bool _seeded = false;
        private readonly object _seedLock = new();

        public Volunteer Add(Volunteer volunteer)
        {
            volunteer.Id = Interlocked.Increment(ref _nextId);
            _volunteers[volunteer.Id] = volunteer;
            return volunteer;
        }

        public IEnumerable<Volunteer> GetAll() => _volunteers.Values.OrderByDescending(v => v.DateRegistered);

        public int Count => _volunteers.Count;

        // Seed the same sample volunteers from the original prototype so the
        // dashboard looks populated immediately, matching the demo data.
        public void SeedIfEmpty()
        {
            if (_seeded) return;
            lock (_seedLock)
            {
                if (_seeded) return;
                Add(new Volunteer { FirstName = "Thandi", LastName = "Mokoena", Email = "thandi@mail.com", Phone = "072 111 2233", Skills = "First Aid,Counselling", Availability = "emergency", Notes = "Available in Gauteng region", DateRegistered = new DateTime(2026, 8, 20) });
                Add(new Volunteer { FirstName = "David", LastName = "van der Merwe", Email = "david@mail.com", Phone = "083 444 5566", Skills = "Logistics,Construction", Availability = "weekends", Notes = "Has own transport", DateRegistered = new DateTime(2026, 8, 18) });
                Add(new Volunteer { FirstName = "Priya", LastName = "Naidoo", Email = "priya@mail.com", Phone = "061 777 8899", Skills = "Medical,First Aid", Availability = "weekdays", Notes = "Registered nurse", DateRegistered = new DateTime(2026, 8, 15) });
                Add(new Volunteer { FirstName = "Sipho", LastName = "Dlamini", Email = "sipho@mail.com", Phone = "079 222 3344", Skills = "Food Distribution,Administration", Availability = "both", Notes = "", DateRegistered = new DateTime(2026, 8, 12) });
                _seeded = true;
            }
        }
    }
}