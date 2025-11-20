using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IVoluntariadoRepository
    {
        Task<IEnumerable<Voluntariado>> GetAllByCandidateIdAsync(Guid candidateId);
        Task AddAsync(Voluntariado voluntariado);
        Task UpdateAsync(Voluntariado voluntariado);
        Task DeleteAsync(Guid id);
    }
}