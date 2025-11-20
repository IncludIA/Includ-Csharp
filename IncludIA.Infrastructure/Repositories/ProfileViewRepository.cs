using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class ProfileViewRepository : IProfileViewRepository
    {
        private readonly OracleDbContext _context;

        public ProfileViewRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProfileView view)
        {
            await _context.ProfileViews.AddAsync(view);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountByCandidateIdAsync(Guid candidateId)
        {
            return await _context.ProfileViews.CountAsync(v => v.CandidateId == candidateId);
        }
    }
}