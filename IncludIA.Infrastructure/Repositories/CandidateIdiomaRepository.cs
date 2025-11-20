using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class CandidateIdiomaRepository : ICandidateIdiomaRepository
    {
        private readonly OracleDbContext _context;

        public CandidateIdiomaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CandidateIdioma>> GetAllAsync()
        {
            return await _context.CandidateIdiomas.Include(ci => ci.Idioma).ToListAsync();
        }

        public async Task<CandidateIdioma?> GetByIdAsync(Guid id)
        {
            return await _context.CandidateIdiomas.FindAsync(id);
        }

        public async Task AddAsync(CandidateIdioma idioma)
        {
            await _context.CandidateIdiomas.AddAsync(idioma);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CandidateIdioma idioma)
        {
            _context.CandidateIdiomas.Update(idioma);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var idioma = await GetByIdAsync(id);
            if (idioma != null)
            {
                _context.CandidateIdiomas.Remove(idioma);
                await _context.SaveChangesAsync();
            }
        }
    }
}