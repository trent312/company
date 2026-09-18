using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using company_backend.Domain.Entities;
using company_backend.Application.Interfaces;

namespace company_backend.Infrastructure.Repositories
{
    public class InMemory : ICompanyRepository
    {
        private static readonly ConcurrentDictionary<int, Company> _companies = new();
        private static int _idCounter;

        public async Task<int> SaveAsync(Company company)
        {
            await Task.Yield();

            var id = Interlocked.Increment(ref _idCounter);
            _companies[id] = company;
            return id;
        }

        public async Task<Company?> GetAsync(int id)
        {
            await Task.Yield();
            return _companies.TryGetValue(id, out var c) ? c : null;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            await Task.Yield();
            return _companies.Values;
        }
    }
}
