using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ISavedJobRepository
    {
        Task<IEnumerable<SavedJob>> GetByCandidateIdAsync(Guid candidateId);
        Task AddAsync(SavedJob saved);
        Task DeleteAsync(Guid id);
    }
}