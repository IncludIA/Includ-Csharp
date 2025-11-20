using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class SavedCandidateRepository : ISavedCandidateRepository
    {
        private readonly OracleDbContext _context;

        public SavedCandidateRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SavedCandidate>> GetByRecruiterIdAsync(Guid recruiterId)
        {
            return await _context.SavedCandidates
                .Include(sc => sc.Candidate)
                .Where(sc => sc.RecruiterId == recruiterId)
                .ToListAsync();
        }

        public async Task AddAsync(SavedCandidate saved)
        {
            await _context.SavedCandidates.AddAsync(saved);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var saved = await _context.SavedCandidates.FindAsync(id);
            if (saved != null)
            {
                _context.SavedCandidates.Remove(saved);
                await _context.SaveChangesAsync();
            }
        }
    }
}