using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IncludIA.Domain.Entities;

[Table("empresas")]
public class Empresa
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string NomeOficial { get; set; }

    public string NomeFantasia { get; set; }

    [Required]
    public string Cnpj { get; set; }

    public string Localizacao { get; set; }

    [MaxLength(4000)]
    public string Descricao { get; set; }

    [MaxLength(4000)]
    public string Cultura { get; set; }

    public string FotoCapaUrl { get; set; }

    [JsonIgnore]
    public ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();

    [JsonIgnore]
    public ICollection<JobVaga> Vagas { get; set; } = new List<JobVaga>();
}