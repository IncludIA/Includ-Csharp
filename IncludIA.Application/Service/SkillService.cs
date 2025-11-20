using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class SkillService
    {
        private readonly ISkillRepository _repository;

        public SkillService(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Skill> GetOrCreateAsync(string nome, Domain.Enums.SkillType tipo)
        {
            var existing = await _repository.GetByNameAsync(nome);
            if (existing != null) return existing;

            var newSkill = new Skill { Id = Guid.NewGuid(), Nome = nome, TipoSkill = tipo };
            await _repository.AddAsync(newSkill);
            return newSkill;
        }
    }
}