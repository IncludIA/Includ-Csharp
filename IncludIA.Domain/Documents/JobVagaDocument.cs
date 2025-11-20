using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IncludIA.Domain.Documents
{
    public class JobVagaDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string EmpresaNome { get; set; }
        public decimal? Salario { get; set; }
        public List<string> Skills { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}