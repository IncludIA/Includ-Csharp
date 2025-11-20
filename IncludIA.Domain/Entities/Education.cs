using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities
{
    [Table("educations")]
    public class Education
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string NomeInstituicao { get; set; }

        [Required]
        public GrauEducacao Grau { get; set; }

        public string AreaEstudo { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        [MaxLength(1000)]
        public string Descricao { get; set; }

        // Relacionamentos
        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}