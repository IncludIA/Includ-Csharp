using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncludIA.Domain.Entities;

[Table("candidates")]
public class Candidate
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

    [MaxLength(2000)]
    public string ResumoPerfil { get; set; }

    [MaxLength(2000)]
    public string ResumoInclusivoIA { get; set; }

    public string FotoPerfilUrl { get; set; }

    public bool IsAtive { get; set; }

    // Relacionamentos
    // Nota: Em C#, inicializamos listas para evitar NullReferenceException
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        
    public ICollection<Experience> Experiencias { get; set; } = new List<Experience>();
        
    public ICollection<Education> Formacoes { get; set; } = new List<Education>();
        
    public ICollection<Voluntariado> Voluntariados { get; set; } = new List<Voluntariado>();
        
    // Supondo que CandidateIdioma seja uma entidade de ligação
    public ICollection<CandidateIdioma> Idiomas { get; set; } = new List<CandidateIdioma>();

    [JsonIgnore] // Evita ciclo infinito na serialização
    public ICollection<Match> Matches { get; set; } = new List<Match>();

    [JsonIgnore]
    public ICollection<SavedJob> VagasSalvas { get; set; } = new List<SavedJob>();

    [JsonIgnore]
    public ICollection<ProfileView> ViewsNoPerfil { get; set; } = new List<ProfileView>();

    [JsonIgnore]
    public ICollection<Notification> Notificacoes { get; set; } = new List<Notification>();
}