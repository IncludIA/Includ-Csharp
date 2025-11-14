using IncludIA.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace IncludIA.Infrastructure.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("IncludIADb");
        }

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
        public IMongoCollection<Candidato> Candidatos => _database.GetCollection<Candidato>("Candidatos");
        public IMongoCollection<Candidatura> Candidaturas => _database.GetCollection<Candidatura>("Candidaturas");
    }
}