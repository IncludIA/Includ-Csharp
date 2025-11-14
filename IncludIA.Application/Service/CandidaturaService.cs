using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;

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
    }
}