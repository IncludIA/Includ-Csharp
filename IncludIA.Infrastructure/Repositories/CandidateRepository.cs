using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly OracleDbContext _context;

        public CandidateRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync(int page, int size)
        {
            return await _context.Candidates
                .AsNoTracking()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Candidate?> GetByIdAsync(Guid id)
        {
            return await _context.Candidates
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidate?> GetByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var candidate = await GetByIdAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
        }
    }
}