using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class EducationService
    {
        private readonly IEducationRepository _repository;

        public EducationService(IEducationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Education>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _repository.GetAllByCandidateIdAsync(candidateId);
        }

        public async Task AddEducationAsync(Education education)
        {
            await _repository.AddAsync(education);
        }

        public async Task UpdateEducationAsync(Education education)
        {
            await _repository.UpdateAsync(education);
        }

        public async Task DeleteEducationAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}