using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ISavedCandidateRepository
    {
        Task<IEnumerable<SavedCandidate>> GetByRecruiterIdAsync(Guid recruiterId);
        Task AddAsync(SavedCandidate saved);
        Task DeleteAsync(Guid id);
    }
}