using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class SavedJobRepository : ISavedJobRepository
    {
        private readonly OracleDbContext _context;

        public SavedJobRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SavedJob>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _context.SavedJobs
                .Include(sj => sj.Vaga)
                .Where(sj => sj.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task AddAsync(SavedJob saved)
        {
            await _context.SavedJobs.AddAsync(saved);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var saved = await _context.SavedJobs.FindAsync(id);
            if (saved != null)
            {
                _context.SavedJobs.Remove(saved);
                await _context.SaveChangesAsync();
            }
        }
    }
}