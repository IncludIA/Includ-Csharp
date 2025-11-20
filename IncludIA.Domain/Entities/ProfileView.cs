using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncludIA.Domain.Entities
{
    [Table("profile_views")]
    public class ProfileView
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

        // Relacionamentos
        [ForeignKey("Recruiter")]
        public Guid RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }

        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}