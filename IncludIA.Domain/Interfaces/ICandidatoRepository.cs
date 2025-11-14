using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ICandidatoRepository
    {
        Task<IEnumerable<Candidato>> GetAllAsync(int page, int pageSize);
        Task<Candidato?> GetByIdAsync(string id);
        Task CreateAsync(Candidato candidato);
        Task<bool> UpdateAsync(Candidato candidato);
        Task<bool> DeleteAsync(string id);
    }
}