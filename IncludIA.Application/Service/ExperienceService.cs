using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class ExperienceService
    {
        private readonly IExperienceRepository _repository;

        public ExperienceService(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Experience>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _repository.GetAllByCandidateIdAsync(candidateId);
        }

        public async Task AddExperienceAsync(Experience experience)
        {
            await _repository.AddAsync(experience);
        }

        public async Task UpdateExperienceAsync(Experience experience)
        {
            await _repository.UpdateAsync(experience);
        }

        public async Task DeleteExperienceAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}