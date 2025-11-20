using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IEducationRepository
    {
        Task<IEnumerable<Education>> GetAllByCandidateIdAsync(Guid candidateId);
        Task<Education?> GetByIdAsync(Guid id);
        Task AddAsync(Education education);
        Task UpdateAsync(Education education);
        Task DeleteAsync(Guid id);
    }
}