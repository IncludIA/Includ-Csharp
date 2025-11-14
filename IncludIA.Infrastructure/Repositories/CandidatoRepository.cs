using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using MongoDB.Driver;

namespace IncludIA.Infrastructure.Repositories
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private readonly IMongoCollection<Candidato> _collection;

        public CandidatoRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Candidato>("Candidatos"); 
        }

        public async Task<IEnumerable<Candidato>> GetAllAsync(int page, int pageSize)
        {
            return await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<Candidato?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Candidato candidato) =>
            await _collection.InsertOneAsync(candidato);
    }
}