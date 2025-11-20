using IncludIA.Domain.Enums;
using IncludIA.Api.DTOs.Common; // Certifique-se de ter uma classe base Resource ou LinkDto

namespace IncludIA.Api.DTOs.Vaga
{
    public class JobVagaResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        
        public string Descricao { get; set; } 
        public bool IsDescricaoInclusiva { get; set; }
        
        public string Localizacao { get; set; }
        public string TipoVaga { get; set; } // String para facilitar leitura no JSON
        public string ModeloTrabalho { get; set; }
        public decimal? SalarioMin { get; set; }
        public decimal? SalarioMax { get; set; }
        public string Beneficios { get; set; }
        public bool IsAtiva { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string NomeEmpresa { get; set; }
        public string NomeRecrutador { get; set; }
        public List<string> Skills { get; set; } = new List<string>();

        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}