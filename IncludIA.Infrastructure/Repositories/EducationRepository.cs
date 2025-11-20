using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly OracleDbContext _context;

        public EducationRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Education>> GetAllByCandidateIdAsync(Guid candidateId)
        {
            return await _context.Educations
                .Where(e => e.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task<Education?> GetByIdAsync(Guid id)
        {
            return await _context.Educations.FindAsync(id);
        }

        public async Task AddAsync(Education education)
        {
            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Education education)
        {
            _context.Educations.Update(education);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var education = await GetByIdAsync(id);
            if (education != null)
            {
                _context.Educations.Remove(education);
                await _context.SaveChangesAsync();
            }
        }
    }
}