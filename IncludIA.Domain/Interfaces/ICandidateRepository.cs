using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync(int page, int size);
        Task<Candidate?> GetByIdAsync(Guid id);
        Task<Candidate?> GetByEmailAsync(string email);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task DeleteAsync(Guid id);
    }
}