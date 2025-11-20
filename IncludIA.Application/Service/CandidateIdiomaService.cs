using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class CandidateIdiomaService
    {
        private readonly ICandidateIdiomaRepository _repository;

        public CandidateIdiomaService(ICandidateIdiomaRepository repository)
        {
            _repository = repository;
        }

        public async Task AddIdiomaToCandidateAsync(CandidateIdioma candidateIdioma)
        {
            await _repository.AddAsync(candidateIdioma);
        }

        public async Task RemoveIdiomaFromCandidateAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}