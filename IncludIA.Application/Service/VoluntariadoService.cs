using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class VoluntariadoService
    {
        private readonly IVoluntariadoRepository _repository;

        public VoluntariadoService(IVoluntariadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Voluntariado>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _repository.GetAllByCandidateIdAsync(candidateId);
        }

        public async Task AddVoluntariadoAsync(Voluntariado voluntariado)
        {
            await _repository.AddAsync(voluntariado);
        }
        
        public async Task UpdateVoluntariadoAsync(Voluntariado voluntariado)
        {
            await _repository.UpdateAsync(voluntariado);
        }

        public async Task DeleteVoluntariadoAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}