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
    public class CandidaturaServiceTests
    {
        private readonly Mock<ICandidaturaRepository> _mockMongoRepo;
        private readonly Mock<OracleDbContext> _mockOracleContext;
        private readonly CandidaturaService _service;

        public CandidaturaServiceTests()
        {
            _mockMongoRepo = new Mock<ICandidaturaRepository>();
            var options = new DbContextOptions<OracleDbContext>();
            _mockOracleContext = new Mock<OracleDbContext>(options);
            
            var mockDbSet = new Mock<DbSet<Candidatura>>();
            _mockOracleContext.Setup(c => c.Candidaturas).Returns(mockDbSet.Object);

            _service = new CandidaturaService(_mockMongoRepo.Object, _mockOracleContext.Object);
        }

        [Fact]
        public async Task CreateAsync_DeveSalvar_EmAmbosOsBancos()
        {
            var candidatura = new Candidatura { CandidatoId = "123", VagaMongoId = "vaga-abc" };
            await _service.CreateAsync(candidatura);
            _mockMongoRepo.Verify(r => r.CreateAsync(candidatura), Times.Once);
            _mockOracleContext.Verify(c => c.Candidaturas.Add(candidatura), Times.Once);
            _mockOracleContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}