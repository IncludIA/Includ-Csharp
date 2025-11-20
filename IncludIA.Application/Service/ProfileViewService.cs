using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class ProfileViewService
    {
        private readonly IProfileViewRepository _repository;

        public ProfileViewService(IProfileViewRepository repository)
        {
            _repository = repository;
        }

        public async Task RegisterViewAsync(Guid recruiterId, Guid candidateId)
        {
            var view = new ProfileView
            {
                Id = Guid.NewGuid(),
                RecruiterId = recruiterId,
                CandidateId = candidateId,
                ViewedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(view);
        }

        public async Task<int> GetProfileViewsCountAsync(Guid candidateId)
        {
            return await _repository.GetCountByCandidateIdAsync(candidateId);
        }
    }
}