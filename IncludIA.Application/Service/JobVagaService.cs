using IncludIA.Domain.Entities;
using IncludIA.Domain.Documents;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using MongoDB.Driver;

namespace IncludIA.Application.Service
{
    public class JobVagaService
    {
        private readonly IJobVagaRepository _repository;
        private readonly MongoDbContext _mongoContext;

        public JobVagaService(IJobVagaRepository repository, MongoDbContext mongoContext)
        {
            _repository = repository;
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<JobVaga>> GetAllAsync(int page, int size)
        {
            return await _repository.GetAllAsync(page, size);
        }

        public async Task<JobVaga?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(JobVaga vaga)
        {
            await _repository.AddAsync(vaga);
            var vagaDoc = new JobVagaDocument
            {
                Id = vaga.Id,
                Titulo = vaga.Titulo,
                Descricao = vaga.DescricaoInclusiva ?? vaga.DescricaoOriginal,
                EmpresaNome = "Empresa (Carregar Nome)", 
                Salario = vaga.SalarioMax,
                CreatedAt = vaga.CreatedAt,
                Skills = vaga.SkillsDesejadas.Select(s => s.Nome).ToList()
            };

            await _mongoContext.Vagas.InsertOneAsync(vagaDoc);
        }

        public async Task UpdateAsync(JobVaga vaga)
        {
            await _repository.UpdateAsync(vaga);
            var filter = Builders<JobVagaDocument>.Filter.Eq(x => x.Id, vaga.Id);
            var update = Builders<JobVagaDocument>.Update
                .Set(x => x.Titulo, vaga.Titulo)
                .Set(x => x.Descricao, vaga.DescricaoInclusiva ?? vaga.DescricaoOriginal)
                .Set(x => x.Salario, vaga.SalarioMax);
            
            await _mongoContext.Vagas.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _mongoContext.Vagas.DeleteOneAsync(x => x.Id == id);
        }
    }
}