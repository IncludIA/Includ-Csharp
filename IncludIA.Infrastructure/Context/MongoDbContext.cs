using IncludIA.Domain.Documents;
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

        public IMongoCollection<JobVagaDocument> Vagas => _database.GetCollection<JobVagaDocument>("vagas");
        public IMongoCollection<NotificationDocument> Notifications => _database.GetCollection<NotificationDocument>("notifications");
        public IMongoCollection<MatchDocument> Matches => _database.GetCollection<MatchDocument>("matches");

        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
    }
}