using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities
{
    [Table("experiences")]
    public class Experience
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TituloCargo { get; set; }

        [Required]
        public TipoContrato TipoEmprego { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        [MaxLength(2000)]
        public string Descricao { get; set; }

        // Relacionamentos
        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        [ForeignKey("Empresa")]
        public Guid? EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
    }
}