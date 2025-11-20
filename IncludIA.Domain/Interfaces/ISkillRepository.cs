using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetAllAsync();
        Task<Skill?> GetByNameAsync(string nome);
        Task AddAsync(Skill skill);
    }
}