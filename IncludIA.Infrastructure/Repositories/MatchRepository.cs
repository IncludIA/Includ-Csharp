using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly OracleDbContext _context;

        public MatchRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<Match?> GetMatchAsync(Guid candidateId, Guid vagaId)
        {
            return await _context.Matches
                .FirstOrDefaultAsync(m => m.CandidateId == candidateId && m.JobVagaId == vagaId);
        }

        public async Task<IEnumerable<Match>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _context.Matches
                .Include(m => m.Vaga) 
                .Where(m => m.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task AddAsync(Match match)
        {
            await _context.Matches.AddAsync(match);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Match match)
        {
            _context.Matches.Update(match);
            await _context.SaveChangesAsync();
        }
    }
}