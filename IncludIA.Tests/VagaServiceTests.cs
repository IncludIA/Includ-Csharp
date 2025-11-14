using Xunit;
using Moq;
using IncludIA.Domain.Interfaces;
using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using IncludIA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IncludIA.Tests
{
    public class VagaServiceTests
    {
        private readonly Mock<IVagaRepository> _mockMongoRepo;
        private readonly Mock<OracleDbContext> _mockOracleContext; 
        private readonly VagaService _service;

        public VagaServiceTests()
        {
            _mockMongoRepo = new Mock<IVagaRepository>();
            var options = new DbContextOptions<OracleDbContext>();
            _mockOracleContext = new Mock<OracleDbContext>(options); 
            _service = new VagaService(_mockMongoRepo.Object, _mockOracleContext.Object);
        }

        [Fact]
        public async Task CreateAsync_DeveChamarRepositorio_UmaVez()
        {
            var vaga = new Vaga { Titulo = "Dev C#" };
            _mockOracleContext.Setup(db => db.Database.ExecuteSqlInterpolatedAsync(
                    It.IsAny<FormattableString>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);
            await _service.CreateAsync(vaga);
            _mockMongoRepo.Verify(r => r.CreateAsync(It.IsAny<Vaga>()), Times.Once);
            _mockOracleContext.Verify(db => db.Database.ExecuteSqlInterpolatedAsync(
                It.IsAny<FormattableString>(), 
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarVaga_QuandoExistir()
        {
            var vagaId = "12345";
            var vagaSimulada = new Vaga { Id = vagaId, Titulo = "Teste" };
            _mockMongoRepo.Setup(r => r.GetByIdAsync(vagaId))
                     .ReturnsAsync(vagaSimulada);
            var vagaRetornada = await _service.GetByIdAsync(vagaId);
            Assert.NotNull(vagaRetornada);
            Assert.Equal(vagaId, vagaRetornada.Id);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNulo_QuandoNaoExistir()
        {
            _mockMongoRepo.Setup(r => r.GetByIdAsync(It.IsAny<string>()))
                     .ReturnsAsync((Vaga)null);
            var vagaRetornada = await _service.GetByIdAsync("id_inexistente");
            Assert.Null(vagaRetornada);
        }
    }
}