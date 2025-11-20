using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class IdiomaRepository : IIdiomaRepository
    {
        private readonly OracleDbContext _context;

        public IdiomaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Idioma>> GetAllAsync()
        {
            return await _context.Idiomas.ToListAsync();
        }

        public async Task<Idioma?> GetByIdAsync(Guid id)
        {
            return await _context.Idiomas.FindAsync(id);
        }

        public async Task AddAsync(Idioma idioma)
        {
            await _context.Idiomas.AddAsync(idioma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var idioma = await GetByIdAsync(id);
            if (idioma != null)
            {
                _context.Idiomas.Remove(idioma);
                await _context.SaveChangesAsync();
            }
        }
    }
}