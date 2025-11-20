using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class CandidateService
    {
        private readonly ICandidateRepository _repository;

        public CandidateService(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync(int page, int size)
        {
            return await _repository.GetAllAsync(page, size);
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Candidate candidate)
        {
            // Aqui entraria hash de senha (ex: BCrypt)
            // candidate.SenhaHash = BCrypt.Net.BCrypt.HashPassword(candidate.SenhaHash);
            await _repository.AddAsync(candidate);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            await _repository.UpdateAsync(candidate);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}