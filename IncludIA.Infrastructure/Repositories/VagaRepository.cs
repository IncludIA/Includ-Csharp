using MongoDB.Driver;
using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;

namespace IncludIA.Infrastructure.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        private readonly IMongoCollection<Vaga> _collection;

        public VagaRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Vaga>("Vagas");
        }

        public async Task<IEnumerable<Vaga>> GetAllAsync(int page, int pageSize)
        {
            return await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<Vaga?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vaga vaga) =>
            await _collection.InsertOneAsync(vaga);

        public async Task UpdateAsync(string id, Vaga vaga) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, vaga);

        public async Task DeleteAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}