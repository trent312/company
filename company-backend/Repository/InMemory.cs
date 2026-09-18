using System.Collections.Concurrent;
using System.Threading;
using company_backend.DTO;

namespace company_backend.Repository
{
    public class InMemory
    {
        // Simple thread-safe in-memory store for companies
        private static readonly ConcurrentDictionary<int, Company> _companies = new();
        private static int _idCounter;

        // Saves the provided company to the in-memory store and returns the generated id.
        public int Save(Company company)
        {
            var id = Interlocked.Increment(ref _idCounter);
            _companies[id] = company;
            return id;
        }

        // Optional: expose read access for other parts of the app
        public Company? Get(int id) => _companies.TryGetValue(id, out var c) ? c : null;
    }
}
