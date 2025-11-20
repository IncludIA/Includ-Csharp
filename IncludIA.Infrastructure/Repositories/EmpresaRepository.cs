using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly OracleDbContext _context;

        public EmpresaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresas.AsNoTracking().ToListAsync();
        }

        public async Task<Empresa?> GetByIdAsync(Guid id)
        {
            return await _context.Empresas.FindAsync(id);
        }
        
        public async Task<Empresa?> GetByCnpjAsync(string cnpj)
        {
            return await _context.Empresas.FirstOrDefaultAsync(e => e.Cnpj == cnpj);
        }

        public async Task AddAsync(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var empresa = await GetByIdAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }
    }
}