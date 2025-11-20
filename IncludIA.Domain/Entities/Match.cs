using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncludIA.Domain.Enums;

namespace IncludIA.Domain.Entities;

[Table("matches")]
public class Match
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Candidate")]
    public Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; }

    [ForeignKey("Vaga")]
    public Guid JobVagaId { get; set; }
    public JobVaga Vaga { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal MatchScore { get; set; }

    [Required]
    public MatchStatus Status { get; set; } // Enum

    public bool IsLikedByCandidate { get; set; }
    public bool IsLikedByRecruiter { get; set; }

    public void UpdateStatus()
    {
        if (IsLikedByCandidate && IsLikedByRecruiter)
        {
            Status = MatchStatus.Matched;
        }
        else if (!IsLikedByCandidate)
        {
            Status = MatchStatus.RejeitadoCandidato;
        }
        else if (!IsLikedByRecruiter)
        {
            Status = MatchStatus.RejeitadoRecrutador;
        }
    }
}