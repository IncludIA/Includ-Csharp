using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IExperienceRepository
    {
        Task<IEnumerable<Experience>> GetAllByCandidateIdAsync(Guid candidateId);
        Task<Experience?> GetByIdAsync(Guid id);
        Task AddAsync(Experience experience);
        Task UpdateAsync(Experience experience);
        Task DeleteAsync(Guid id);
    }
}