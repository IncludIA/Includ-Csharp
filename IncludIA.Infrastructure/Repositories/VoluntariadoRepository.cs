using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class VoluntariadoRepository : IVoluntariadoRepository
    {
        private readonly OracleDbContext _context;

        public VoluntariadoRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voluntariado>> GetAllByCandidateIdAsync(Guid candidateId)
        {
            return await _context.Voluntariados
                .Where(v => v.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task AddAsync(Voluntariado voluntariado)
        {
            await _context.Voluntariados.AddAsync(voluntariado);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Voluntariado voluntariado)
        {
            _context.Voluntariados.Update(voluntariado);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var voluntariado = await _context.Voluntariados.FindAsync(id);
            if (voluntariado != null)
            {
                _context.Voluntariados.Remove(voluntariado);
                await _context.SaveChangesAsync();
            }
        }
    }
}