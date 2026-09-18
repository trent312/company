using company_backend.Domain.Entities;

namespace company_backend.Application.Interfaces
{
    public interface ICompanyRepository
    {
        System.Threading.Tasks.Task<int> SaveAsync(Company company);
        System.Threading.Tasks.Task<Company?> GetAsync(int id);
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Company>> GetAllAsync();
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<Company>> SearchAsync(string? query);
    }
}
