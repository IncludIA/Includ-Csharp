using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class SavedCandidateService
    {
        private readonly ISavedCandidateRepository _repository;

        public SavedCandidateService(ISavedCandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SavedCandidate>> GetSavedCandidatesAsync(Guid recruiterId)
        {
            return await _repository.GetByRecruiterIdAsync(recruiterId);
        }

        public async Task SaveCandidateAsync(Guid recruiterId, Guid candidateId)
        {
            var saved = new SavedCandidate
            {
                Id = Guid.NewGuid(),
                RecruiterId = recruiterId,
                CandidateId = candidateId,
                SavedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(saved);
        }

        public async Task UnsaveCandidateAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}