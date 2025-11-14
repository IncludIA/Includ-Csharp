using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncludIA.Domain.Entities
{
    [Table("TB_CANDIDATURA")]
    public class Candidatura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Key]
        [Column("ID_CANDIDATURA")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("ID_CANDIDATO")]
        [Required]
        public string CandidatoId { get; set; } = string.Empty;
        public virtual Candidato? Candidato { get; set; }

        [Column("ID_VAGA_MONGO")]
        [Required]
        public string VagaMongoId { get; set; } = string.Empty;
        
        [Column("DT_APLICACAO")]
        public DateTime DataAplicacao { get; set; } = DateTime.UtcNow;
    }
}