using IncludIA.Domain.Entities;
using IncludIA.Domain.Documents;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using IncludIA.Domain.Enums;

namespace IncludIA.Application.Service
{
    public class MatchService
    {
        private readonly IMatchRepository _repository;
        private readonly MongoDbContext _mongoContext;

        public MatchService(IMatchRepository repository, MongoDbContext mongoContext)
        {
            _repository = repository;
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<Match>> GetByCandidateIdAsync(Guid candidateId)
        {
            return await _repository.GetByCandidateIdAsync(candidateId);
        }

        public async Task CreateMatchAsync(Match match)
        {
            if (match.Status == 0) match.Status = MatchStatus.Pendente;
            await _repository.AddAsync(match);
            var matchDoc = new MatchDocument
            {
                Id = match.Id,
                CandidateId = match.CandidateId,
                JobVagaId = match.JobVagaId,
                Score = (double)match.MatchScore,
                Status = match.Status.ToString(),
                MatchDate = DateTime.UtcNow
            };

            await _mongoContext.Matches.InsertOneAsync(matchDoc);
        }

        public async Task UpdateMatchStatusAsync(Match match)
        {
            match.UpdateStatus(); 
            await _repository.UpdateAsync(match);
            var filter = MongoDB.Driver.Builders<MatchDocument>.Filter.Eq(x => x.Id, match.Id);
            var update = MongoDB.Driver.Builders<MatchDocument>.Update.Set(x => x.Status, match.Status.ToString());
            await _mongoContext.Matches.UpdateOneAsync(filter, update);
        }
    }
}