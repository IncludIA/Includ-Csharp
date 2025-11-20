using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly OracleDbContext _context;

        public RecruiterRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recruiter>> GetAllAsync()
        {
            return await _context.Recruiters.AsNoTracking().ToListAsync();
        }

        public async Task<Recruiter?> GetByIdAsync(Guid id)
        {
            return await _context.Recruiters
                .Include(r => r.Empresa)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        
        public async Task<Recruiter?> GetByEmailAsync(string email)
        {
            return await _context.Recruiters.FirstOrDefaultAsync(r => r.Email == email);
        }

        public async Task AddAsync(Recruiter recruiter)
        {
            await _context.Recruiters.AddAsync(recruiter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Recruiter recruiter)
        {
            _context.Recruiters.Update(recruiter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var recruiter = await GetByIdAsync(id);
            if (recruiter != null)
            {
                _context.Recruiters.Remove(recruiter);
                await _context.SaveChangesAsync();
            }
        }
    }
}