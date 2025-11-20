using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa?> GetByIdAsync(Guid id);
        Task<Empresa?> GetByCnpjAsync(string cnpj);
        Task AddAsync(Empresa empresa);
        Task UpdateAsync(Empresa empresa);
        Task DeleteAsync(Guid id);
    }
}