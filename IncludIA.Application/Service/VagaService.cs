using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IncludIA.Application.Service
{
    public class VagaService
    {
        private readonly IVagaRepository _repositoryMongo; 
        private readonly OracleDbContext _contextOracle;   

        public VagaService(IVagaRepository repositoryMongo, OracleDbContext contextOracle)
        {
            _repositoryMongo = repositoryMongo;
            _contextOracle = contextOracle;
        }

        public VagaService(IVagaRepository mockRepoObject)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Vaga vaga)
        {
            await _repositoryMongo.CreateAsync(vaga);
            var pTitulo = vaga.Titulo;
            var pDescricao = vaga.Descricao;
            var pEmpresa = vaga.Empresa;
            var pRemoto = vaga.Remoto ? 1 : 0;
            
            await _contextOracle.Database.ExecuteSqlInterpolatedAsync(
                $"BEGIN PROC_INSERT_VAGA(p_titulo => {pTitulo}, p_descricao => {pDescricao}, p_empresa => {pEmpresa}, p_remoto => {pRemoto}); END;"
            );
        }
        
        public Task<IEnumerable<Vaga>> GetAllAsync(int page, int pageSize) => 
            _repositoryMongo.GetAllAsync(page, pageSize);

        public Task<Vaga?> GetByIdAsync(string id) => 
            _repositoryMongo.GetByIdAsync(id);

        

        public async Task UpdateAsync(string id, Vaga vaga)
        {
            await _repositoryMongo.UpdateAsync(id, vaga);
        }

        public async Task DeleteAsync(string id)
        {
            await _repositoryMongo.DeleteAsync(id);
        }
    }
}