using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class SavedJobService
    {
        private readonly ISavedJobRepository _repository;

        public SavedJobService(ISavedJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SavedJob>> GetSavedJobsAsync(Guid candidateId)
        {
            return await _repository.GetByCandidateIdAsync(candidateId);
        }

        public async Task SaveJobAsync(Guid candidateId, Guid vagaId)
        {
            var savedJob = new SavedJob
            {
                Id = Guid.NewGuid(),
                CandidateId = candidateId,
                JobVagaId = vagaId,
                SavedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(savedJob);
        }

        public async Task UnsaveJobAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}