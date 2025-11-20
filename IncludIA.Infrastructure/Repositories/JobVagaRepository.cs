using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Infrastructure.Repositories
{
    public class JobVagaRepository : IJobVagaRepository
    {
        private readonly OracleDbContext _context;

        public JobVagaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobVaga>> GetAllAsync(int page, int size)
        {
            return await _context.JobVagas
                .AsNoTracking()
                .Include(v => v.Empresa)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<JobVaga?> GetByIdAsync(Guid id)
        {
            return await _context.JobVagas
                .Include(v => v.Empresa)
                .Include(v => v.SkillsDesejadas)
                .FirstOrDefaultAsync(v => v.Id == id);
        }
        
        public async Task<IEnumerable<JobVaga>> GetByRecruiterIdAsync(Guid recruiterId)
        {
            return await _context.JobVagas
                .Where(v => v.RecruiterId == recruiterId)
                .ToListAsync();
        }

        public async Task AddAsync(JobVaga vaga)
        {
            await _context.JobVagas.AddAsync(vaga);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobVaga vaga)
        {
            _context.JobVagas.Update(vaga);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var vaga = await GetByIdAsync(id);
            if (vaga != null)
            {
                _context.JobVagas.Remove(vaga);
                await _context.SaveChangesAsync();
            }
        }
    }
}