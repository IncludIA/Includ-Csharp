using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;

namespace IncludIA.Application.Service
{
    public class CandidatoService
    {
        private readonly ICandidatoRepository _repoMongo;
        private readonly OracleDbContext _contextOracle;

        public CandidatoService(ICandidatoRepository repoMongo, OracleDbContext contextOracle)
        {
            _repoMongo = repoMongo;
            _contextOracle = contextOracle;
        }

        public async Task CreateAsync(Candidato candidato)
        {
            await _repoMongo.CreateAsync(candidato);
            _contextOracle.Candidatos.Add(candidato);
            await _contextOracle.SaveChangesAsync();
        }

        public Task<IEnumerable<Candidato>> GetAllAsync(int page, int pageSize) =>
            _repoMongo.GetAllAsync(page, pageSize);

        public Task<Candidato?> GetByIdAsync(string id) =>
            _repoMongo.GetByIdAsync(id);
    }
}