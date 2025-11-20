using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncludIA.Domain.Entities
{
    [Table("saved_jobs")]
    public class SavedJob
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        [ForeignKey("JobVaga")]
        public Guid JobVagaId { get; set; }
        public JobVaga Vaga { get; set; }
    }
}