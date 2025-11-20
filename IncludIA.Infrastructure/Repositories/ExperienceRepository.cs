using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly OracleDbContext _context;

        public ExperienceRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Experience>> GetAllByCandidateIdAsync(Guid candidateId)
        {
            return await _context.Experiences
                .Where(e => e.CandidateId == candidateId)
                .ToListAsync();
        }

        public async Task<Experience?> GetByIdAsync(Guid id)
        {
            return await _context.Experiences.FindAsync(id);
        }

        public async Task AddAsync(Experience experience)
        {
            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Experience experience)
        {
            _context.Experiences.Update(experience);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var experience = await GetByIdAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
                await _context.SaveChangesAsync();
            }
        }
    }
}