using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace IncludIA.Tests
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _repositoryMock;
        private readonly Mock<HttpMessageHandler> _httpHandlerMock;
        private readonly InclusaoIAService _iaService;
        private readonly CandidateService _service;

        public CandidateServiceTests()
        {
            _repositoryMock = new Mock<ICandidateRepository>();

            _httpHandlerMock = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_httpHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api-ia-mock.com/")
            };
            
            var configMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            configMock.Setup(x => x["InclusaoIA:ApiUrl"]).Returns("https://api-ia-mock.com/");

            _iaService = new InclusaoIAService(httpClient);
            _service = new CandidateService(_repositoryMock.Object, _iaService);
        }

        [Fact]
        public async Task CreateAsync_DeveCriptografarSenha_QuandoSucesso()
        {
            var candidato = new Candidate
            {
                Id = Guid.NewGuid(),
                Nome = "Teste",
                Email = "teste@email.com",
                SenhaHash = "senha123",
                ResumoPerfil = "Perfil seguro"
            };
            SetupIaResponse(true, "Perfil seguro anonimizado");
            
            await _service.CreateAsync(candidato);
            
            Assert.NotEqual("senha123", candidato.SenhaHash);
            Assert.StartsWith("$2", candidato.SenhaHash); 
            
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Candidate>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_DeveBloquear_QuandoIaDetectaOdio()
        {
            var candidato = new Candidate { Nome = "Hater", SenhaHash = "123", ResumoPerfil = "Conteúdo tóxico" };
            
            SetupIaResponse(false, null, "Discurso de ódio detectado");
            
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(candidato));
            
            Assert.Contains("reprovado pela nossa IA", ex.Message);
            
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Candidate>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_DevePreencherResumoInclusivo()
        {
            var candidato = new Candidate { Nome = "Bom", SenhaHash = "123", ResumoPerfil = "Meu perfil com idade" };
            var resumoAnonimizado = "Perfil sem dados pessoais";
            
            SetupIaResponse(true, resumoAnonimizado);
            
            await _service.CreateAsync(candidato);
            
            Assert.Equal(resumoAnonimizado, candidato.ResumoInclusivoIA);
        }

        private void SetupIaResponse(bool aprovado, string? resumoAnonimizado = null, string? motivo = null)
        {
            var responseModeracao = new AnaliseSegurancaResponse { Aprovado = aprovado, Motivo = motivo };
            var responseAnonimizacao = new AnonimizacaoResponse { ResumoProfissional = resumoAnonimizado ?? "Texto Padrão" };

            _httpHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("moderar")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(responseModeracao)
                });

            _httpHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("anonimizar")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(responseAnonimizacao)
                });
        }
    }
}