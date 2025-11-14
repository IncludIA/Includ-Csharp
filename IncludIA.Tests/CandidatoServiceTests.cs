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
    public class CandidatoServiceTests
    {
        private readonly Mock<ICandidatoRepository> _mockMongoRepo;
        private readonly Mock<OracleDbContext> _mockOracleContext;
        private readonly CandidatoService _service;

        public CandidatoServiceTests()
        {
            _mockMongoRepo = new Mock<ICandidatoRepository>();
            var options = new DbContextOptions<OracleDbContext>();
            _mockOracleContext = new Mock<OracleDbContext>(options);
            var mockDbSet = new Mock<DbSet<Candidato>>();
            _mockOracleContext.Setup(c => c.Candidatos).Returns(mockDbSet.Object);
            _service = new CandidatoService(_mockMongoRepo.Object, _mockOracleContext.Object);
        }

        [Fact]
        public async Task CreateAsync_DeveSalvar_EmAmbosOsBancos()
        {
            var candidato = new Candidato { Nome = "Teste", Email = "teste@fiap.com.br" };
            await _service.CreateAsync(candidato);
            _mockMongoRepo.Verify(r => r.CreateAsync(candidato), Times.Once);
            _mockOracleContext.Verify(c => c.Candidatos.Add(candidato), Times.Once);
            _mockOracleContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}