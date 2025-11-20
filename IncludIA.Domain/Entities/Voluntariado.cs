using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncludIA.Domain.Entities
{
    [Table("voluntariados")]
    public class Voluntariado
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Organizacao { get; set; }

        [Required]
        public string Funcao { get; set; }

        [MaxLength(2000)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        // Relacionamentos
        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}