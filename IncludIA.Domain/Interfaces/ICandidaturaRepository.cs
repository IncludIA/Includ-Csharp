using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ICandidaturaRepository
    {
        Task<IEnumerable<Candidatura>> GetAllAsync(int page, int pageSize);
        Task<Candidatura?> GetByIdAsync(string id);
        Task CreateAsync(Candidatura candidatura);
    }
}