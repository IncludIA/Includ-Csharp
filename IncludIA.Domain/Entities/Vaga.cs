using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IncludIA.Domain.Entities
{
    public class Vaga
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public bool Remoto { get; set; }
        public List<string> SkillsObrigatorias { get; set; } = new();
        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;
    }
}