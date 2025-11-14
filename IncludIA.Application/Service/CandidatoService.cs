using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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
        
        public async Task<bool> UpdateAsync(string id, Candidato candidatoIn)
        { 
            candidatoIn.Id = id;
            var mongoResult = await _repoMongo.UpdateAsync(candidatoIn);
            _contextOracle.Candidatos.Update(candidatoIn);
            var oracleResult = await _contextOracle.SaveChangesAsync();
            return mongoResult && oracleResult > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var mongoResult = await _repoMongo.DeleteAsync(id);
            var candidatoOracle = await _contextOracle.Candidatos.FindAsync(id);
            if (candidatoOracle == null)
            {
                return mongoResult; 
            }
            _contextOracle.Candidatos.Remove(candidatoOracle);
            var oracleResult = await _contextOracle.SaveChangesAsync();

            return mongoResult && oracleResult > 0;
        }
    }
}