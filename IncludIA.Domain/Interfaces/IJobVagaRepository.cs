using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IJobVagaRepository
    {
        Task<IEnumerable<JobVaga>> GetAllAsync(int page, int size);
        Task<JobVaga?> GetByIdAsync(Guid id);
        Task<IEnumerable<JobVaga>> GetByRecruiterIdAsync(Guid recruiterId);
        Task AddAsync(JobVaga vaga);
        Task UpdateAsync(JobVaga vaga);
        Task DeleteAsync(Guid id);
    }
}