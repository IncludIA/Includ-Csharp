using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities
{
    [Table("notifications")]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public NotificationType Tipo { get; set; }

        [Required]
        public string Mensagem { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        [ForeignKey("Recruiter")]
        public Guid? RecruiterId { get; set; }
        public Recruiter? Recruiter { get; set; }

        [ForeignKey("Candidate")]
        public Guid? CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
    }
}