using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Services
{
    public class VagaService
    {
        private readonly IVagaRepository _repository;

        public VagaService(IVagaRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Vaga>> GetAllAsync(int page, int pageSize) => 
            _repository.GetAllAsync(page, pageSize);

        public Task<Vaga?> GetByIdAsync(string id) => 
            _repository.GetByIdAsync(id);

        public Task CreateAsync(Vaga vaga) => 
            _repository.CreateAsync(vaga);

        public Task UpdateAsync(string id, Vaga vaga) => 
            _repository.UpdateAsync(id, vaga);

        public Task DeleteAsync(string id) => 
            _repository.DeleteAsync(id);
    }
}