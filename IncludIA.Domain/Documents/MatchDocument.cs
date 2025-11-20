using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IncludIA.Domain.Documents
{
    public class MatchDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobVagaId { get; set; }
        public double Score { get; set; }
        public string Status { get; set; }
        public DateTime MatchDate { get; set; } = DateTime.UtcNow;
    }
}