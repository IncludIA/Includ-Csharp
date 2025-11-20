using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities
{
    [Table("candidate_idiomas")]
    public class CandidateIdioma
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public ProficiencyLevel NivelProficiencia { get; set; }

        // Relacionamentos
        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        [ForeignKey("Idioma")]
        public Guid IdiomaId { get; set; }
        public Idioma Idioma { get; set; } // Certifique-se de criar a classe Idioma também
    }
}