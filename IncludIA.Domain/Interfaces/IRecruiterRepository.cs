using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IRecruiterRepository
    {
        Task<IEnumerable<Recruiter>> GetAllAsync();
        Task<Recruiter?> GetByIdAsync(Guid id);
        Task<Recruiter?> GetByEmailAsync(string email);
        Task AddAsync(Recruiter recruiter);
        Task UpdateAsync(Recruiter recruiter);
        Task DeleteAsync(Guid id);
    }
}