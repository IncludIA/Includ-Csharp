using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IProfileViewRepository
    {
        Task AddAsync(ProfileView view);
        Task<int> GetCountByCandidateIdAsync(Guid candidateId);
    }
}