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

        public async Task<IEnumerable<Company>> SearchAsync(string? query)
        {
            await Task.Yield();

            if (string.IsNullOrWhiteSpace(query))
                return _companies.Values;

            var q = query.Trim().ToLowerInvariant();

            var results = _companies.Values.Where(c =>
            {
                if (!string.IsNullOrWhiteSpace(c.CompanyName) && c.CompanyName.ToLowerInvariant().Contains(q))
                    return true;

                if (!string.IsNullOrWhiteSpace(c.WebSiteUrl) && Uri.TryCreate(c.WebSiteUrl, UriKind.Absolute, out var uri))
                {
                    var host = uri.Host.ToLowerInvariant();
                    if (host.Contains(q)) return true;
                    // also allow checking domain without subdomain
                    var hostParts = host.Split('.');
                    if (hostParts.Length >= 2)
                    {
                        var domain = string.Join('.', hostParts.Skip(hostParts.Length - 2));
                        if (domain.Contains(q)) return true;
                    }
                }

                return false;
            });

            return results;
        }
    }
}
