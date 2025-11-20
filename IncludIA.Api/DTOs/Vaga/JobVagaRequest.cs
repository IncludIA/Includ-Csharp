using System.ComponentModel.DataAnnotations;
using IncludIA.Domain.Enums;

namespace IncludIA.Api.DTOs.Vaga
{
    public class JobVagaRequest
    {
        [Required(ErrorMessage = "O título da vaga é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(5000, ErrorMessage = "A descrição deve ter no máximo 5000 caracteres.")]
        public string DescricaoOriginal { get; set; }

        public string Localizacao { get; set; }

        [Required]
        public TipoContrato TipoVaga { get; set; }

        [Required]
        public ModeloTrabalho ModeloTrabalho { get; set; }

        public decimal? SalarioMin { get; set; }
        public decimal? SalarioMax { get; set; }

        public string Beneficios { get; set; }
        public string ExperienciaRequerida { get; set; }

        [Required]
        public Guid RecruiterId { get; set; }

        [Required]
        public Guid EmpresaId { get; set; }

        public List<Guid> SkillsIds { get; set; } = new List<Guid>();
    }
}