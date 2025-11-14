using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Application.Service
{
    public class CandidaturaService
    {
        private readonly ICandidaturaRepository _repoMongo;
        private readonly OracleDbContext _contextOracle;

        public CandidaturaService(ICandidaturaRepository repoMongo, OracleDbContext contextOracle)
        {
            _repoMongo = repoMongo;
            _contextOracle = contextOracle;
        }

        public async Task CreateAsync(Candidatura candidatura)
        {
            await _repoMongo.CreateAsync(candidatura);

            _contextOracle.Candidaturas.Add(candidatura);
            await _contextOracle.SaveChangesAsync();
        }

        public Task<IEnumerable<Candidatura>> GetAllAsync(int page, int pageSize) =>
            _repoMongo.GetAllAsync(page, pageSize);

        public Task<Candidatura?> GetByIdAsync(string id) =>
            _repoMongo.GetByIdAsync(id);
        
        public async Task<bool> UpdateAsync(string id, Candidatura candidaturaIn)
        {
            candidaturaIn.Id = id;
            var mongoResult = await _repoMongo.UpdateAsync(candidaturaIn);
            _contextOracle.Candidaturas.Update(candidaturaIn);
            var oracleResult = await _contextOracle.SaveChangesAsync();
            return mongoResult && oracleResult > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var mongoResult = await _repoMongo.DeleteAsync(id);
            var candidaturaOracle = await _contextOracle.Candidaturas.FindAsync(id);
            if (candidaturaOracle == null)
            {
                return mongoResult;
            }
            _contextOracle.Candidaturas.Remove(candidaturaOracle);
            var oracleResult = await _contextOracle.SaveChangesAsync();
            return mongoResult && oracleResult > 0;
        }
    }
}