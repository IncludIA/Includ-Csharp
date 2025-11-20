using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IncludIA.Domain.Documents
{
    public class NotificationDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public Guid? UserId { get; set; } // Pode ser Candidato ou Recrutador
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}