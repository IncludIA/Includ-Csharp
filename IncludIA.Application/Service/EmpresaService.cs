using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;

namespace IncludIA.Application.Service
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Empresa?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task CreateAsync(Empresa empresa) => await _repository.AddAsync(empresa);
        public async Task UpdateAsync(Empresa empresa) => await _repository.UpdateAsync(empresa);
        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}