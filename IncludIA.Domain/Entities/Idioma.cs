using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities
{
    [Table("idiomas")]
    public class Idioma
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public NomeIdioma Nome { get; set; }

        [JsonIgnore]
        public ICollection<CandidateIdioma> CandidateIdiomas { get; set; } = new List<CandidateIdioma>();
    }
}