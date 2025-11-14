using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace IncludIA.Domain.Entities
{
    [Table("TB_CANDIDATO")]
    public class Candidato
    {
        [BsonId] 
        [BsonRepresentation(BsonType.ObjectId)]
        [Key]
        [Column("ID_CANDIDATO")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("NOME")]
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Column("EMAIL")]
        [Required]
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();
    }
}