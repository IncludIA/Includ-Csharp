using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using MongoDB.Driver;

namespace IncludIA.Infrastructure.Repositories
{
    public class CandidaturaRepository : ICandidaturaRepository
    {
        private readonly IMongoCollection<Candidatura> _collection;

        public CandidaturaRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<Candidatura>("Candidaturas");
        }

        public async Task<IEnumerable<Candidatura>> GetAllAsync(int page, int pageSize)
        {
            return await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<Candidatura?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Candidatura candidatura) =>
            await _collection.InsertOneAsync(candidatura);
    }
}