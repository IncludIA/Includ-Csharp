using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities
{
    [Table("skills")]
    public class Skill
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public SkillType TipoSkill { get; set; }

        // Relacionamentos Many-to-Many
        [JsonIgnore]
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

        [JsonIgnore]
        public ICollection<JobVaga> Vagas { get; set; } = new List<JobVaga>();
    }
}