using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncludIA.Domain.Entities;

[Table("recruiters")]
public class Recruiter
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string SenhaHash { get; set; }

    public bool IsAtive { get; set; }

    public string FotoPerfilUrl { get; set; }

    [ForeignKey("Empresa")]
    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }

    [JsonIgnore]
    public ICollection<JobVaga> VagasPostadas { get; set; } = new List<JobVaga>();

    [JsonIgnore]
    public ICollection<SavedCandidate> CandidatosSalvos { get; set; } = new List<SavedCandidate>();

    [JsonIgnore]
    public ICollection<ProfileView> ViewsEmCandidatos { get; set; } = new List<ProfileView>();

    [JsonIgnore]
    public ICollection<Notification> Notificacoes { get; set; } = new List<Notification>();
}