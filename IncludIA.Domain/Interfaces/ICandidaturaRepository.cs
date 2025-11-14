using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ICandidaturaRepository
    {
        Task<IEnumerable<Candidatura>> GetAllAsync(int page, int pageSize);
        Task<Candidatura?> GetByIdAsync(string id);
        Task CreateAsync(Candidatura candidatura);
        Task<bool> UpdateAsync(Candidatura candidatura);
        Task<bool> DeleteAsync(string id);
    }
}