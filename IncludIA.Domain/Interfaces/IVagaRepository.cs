using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IVagaRepository
    {
        Task<IEnumerable<Vaga>> GetAllAsync(int page, int pageSize);
        Task<Vaga?> GetByIdAsync(string id);
        Task CreateAsync(Vaga vaga);
        Task UpdateAsync(string id, Vaga vaga);
        Task DeleteAsync(string id);
    }
}