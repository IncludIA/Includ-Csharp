using Xunit;
using Moq;
using IncludIA.Domain.Interfaces;
using IncludIA.Domain.Entities;
using System.Threading.Tasks;
using IncludIA.Application.Services;

namespace IncludIA.Tests
{
    public class VagaServiceTests
    {
        private readonly Mock<IVagaRepository> _mockRepo;
        private readonly VagaService _service;

        public VagaServiceTests()
        {
            _mockRepo = new Mock<IVagaRepository>();
            _service = new VagaService(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateAsync_DeveChamarRepositorio_UmaVez()
        {
            var vaga = new Vaga 
            { 
                Titulo = "Dev .NET Senior", 
                Empresa = "FIAP" 
            };
            await _service.CreateAsync(vaga);
            _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Vaga>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarVaga_QuandoExistir()
        {
            var vagaId = "12345";
            var vagaSimulada = new Vaga 
            { 
                Id = vagaId, 
                Titulo = "Teste" 
            };
            _mockRepo.Setup(r => r.GetByIdAsync(vagaId))
                     .ReturnsAsync(vagaSimulada);

            var vagaRetornada = await _service.GetByIdAsync(vagaId);

            Assert.NotNull(vagaRetornada); 
            Assert.Equal(vagaId, vagaRetornada.Id); 
            Assert.Equal("Teste", vagaRetornada.Titulo); 
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNulo_QuandoNaoExistir()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                     .ReturnsAsync((Vaga)null);

            var vagaRetornada = await _service.GetByIdAsync("id_inexistente");
            Assert.Null(vagaRetornada);
        }
    }
}