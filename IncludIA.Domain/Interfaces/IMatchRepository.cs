using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface IMatchRepository
    {
        Task<Match?> GetMatchAsync(Guid candidateId, Guid vagaId);
        Task<IEnumerable<Match>> GetByCandidateIdAsync(Guid candidateId);
        Task AddAsync(Match match);
        Task UpdateAsync(Match match);
    }
}