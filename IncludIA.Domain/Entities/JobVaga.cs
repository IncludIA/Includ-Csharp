using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using IncludIA.Domain.Enums;
namespace IncludIA.Domain.Entities;

[Table("job_vagas")]
public class JobVaga
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Titulo { get; set; }

    [MaxLength(5000)]
    public string DescricaoOriginal { get; set; }

    [MaxLength(5000)]
    public string DescricaoInclusiva { get; set; }

    public string Localizacao { get; set; }

    [Required]
    public TipoContrato TipoVaga { get; set; }

    [Required] public ModeloTrabalho ModeloTrabalho { get; set; }

    public decimal? SalarioMin { get; set; }

    public decimal? SalarioMax { get; set; }

    public string Beneficios { get; set; }

    public string ExperienciaRequerida { get; set; }

    public bool IsAtiva { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    [ForeignKey("Recruiter")]
    public Guid RecruiterId { get; set; }
    public Recruiter Recruiter { get; set; }

    [ForeignKey("Empresa")]
    public Guid EmpresaId { get; set; }
    public Empresa Empresa { get; set; }

    public ICollection<Skill> SkillsDesejadas { get; set; } = new List<Skill>();

    [JsonIgnore]
    public ICollection<Match> Matches { get; set; } = new List<Match>();
}