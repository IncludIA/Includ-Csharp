using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace IncludIA.Application.Service
{
    public class InclusaoIAService
    {
        private readonly HttpClient _httpClient;
        
        private const string BaseUrl = "https://app-includia-iot-2771.azurewebsites.net";

        public InclusaoIAService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<AnaliseSegurancaResponse?> ModerarConteudoAsync(string texto, string contexto = "perfil_candidato")
        {
            try
            {
                var payload = new { texto_usuario = texto, contexto };
                var response = await _httpClient.PostAsJsonAsync("/api/v1/seguranca/moderar", payload);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AnaliseSegurancaResponse>();
                }
            }
            catch (Exception ex)
            {
                return new AnaliseSegurancaResponse { Aprovado = true }; 
            }
            return new AnaliseSegurancaResponse { Aprovado = true };
        }

        public async Task<string> AnonimizarCurriculoAsync(string textoOriginal)
        {
            try
            {
                var payload = new { texto_curriculo = textoOriginal };
                var response = await _httpClient.PostAsJsonAsync("/api/v1/candidatos/anonimizar", payload);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<AnonimizacaoResponse>();
                    return resultado?.ResumoProfissional ?? textoOriginal;
                }
            }
            catch
            {
            }
            return textoOriginal;
        }

        public async Task<string> GerarDescricaoInclusivaAsync(string cargo, string descricaoOriginal)
        {
            try
            {
                var payload = new { cargo, descricao_original = descricaoOriginal };
                var response = await _httpClient.PostAsJsonAsync("/api/v1/vagas/inclusiva", payload);

                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<VagaInclusivaResponse>();
                    return resultado?.TextoInclusivo ?? descricaoOriginal;
                }
            }
            catch
            {
            }
            return descricaoOriginal;
        }
    }

    public class AnaliseSegurancaResponse 
    { 
        public bool Aprovado { get; set; } 
        public string? Motivo { get; set; }
        public int ScoreSeguranca { get; set; }
    }

    public class AnonimizacaoResponse 
    { 
        public string? ResumoProfissional { get; set; }
        public List<string> HardSkills { get; set; }
    }

    public class VagaInclusivaResponse 
    { 
        public string? TextoInclusivo { get; set; } 
    }
}