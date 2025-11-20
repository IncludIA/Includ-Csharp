using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IIdiomaRepository
    {
        Task<IEnumerable<Idioma>> GetAllAsync();
        Task<Idioma?> GetByIdAsync(Guid id);
        Task AddAsync(Idioma idioma);
        Task DeleteAsync(Guid id);
    }
}