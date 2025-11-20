using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class RecruiterService
    {
        private readonly IRecruiterRepository _repository;

        public RecruiterService(IRecruiterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Recruiter>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Recruiter?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task CreateAsync(Recruiter recruiter) => await _repository.AddAsync(recruiter);
        public async Task UpdateAsync(Recruiter recruiter) => await _repository.UpdateAsync(recruiter);
        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}