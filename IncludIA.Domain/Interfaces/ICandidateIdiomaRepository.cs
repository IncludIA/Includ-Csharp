using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ICandidateIdiomaRepository
    {
        Task<IEnumerable<CandidateIdioma>> GetAllAsync();
        Task<CandidateIdioma?> GetByIdAsync(Guid id);
        Task AddAsync(CandidateIdioma idioma);
        Task UpdateAsync(CandidateIdioma idioma);
        Task DeleteAsync(Guid id);
    }
}