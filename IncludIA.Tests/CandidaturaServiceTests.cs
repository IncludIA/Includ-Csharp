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
        private readonly Mock<DbSet<Candidatura>> _mockCandidaturaDbSet;
        private readonly CandidaturaService _service;

        public CandidaturaServiceTests()
        {
            _mockMongoRepo = new Mock<ICandidaturaRepository>();
            var options = new DbContextOptions<OracleDbContext>();
            _mockOracleContext = new Mock<OracleDbContext>(options);
            _mockCandidaturaDbSet = new Mock<DbSet<Candidatura>>();
            _mockOracleContext.Setup(c => c.Candidaturas).Returns(_mockCandidaturaDbSet.Object);
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

        [Fact]
        public async Task UpdateAsync_DeveAtualizar_EmAmbosOsBancos()
        {
            var candidatura = new Candidatura { Id = "c-123", CandidatoId = "123", VagaMongoId = "vaga-xyz" };
            _mockMongoRepo.Setup(r => r.UpdateAsync(candidatura)).ReturnsAsync(true);
            _mockOracleContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _service.UpdateAsync("c-123", candidatura);
            Assert.True(result);
            _mockMongoRepo.Verify(r => r.UpdateAsync(candidatura), Times.Once);
            _mockOracleContext.Verify(c => c.Candidaturas.Update(candidatura), Times.Once);
            _mockOracleContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once); 
        }

        [Fact]
        public async Task DeleteAsync_DeveDeletar_DeAmbosOsBancos()
        {
            var candidaturaId = "c-123";
            var candidaturaParaDeletar = new Candidatura { Id = candidaturaId };
            _mockMongoRepo.Setup(r => r.DeleteAsync(candidaturaId)).ReturnsAsync(true);
            _mockCandidaturaDbSet.Setup(s => s.FindAsync(candidaturaId)).ReturnsAsync(candidaturaParaDeletar);
            _mockOracleContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _service.DeleteAsync(candidaturaId);
            Assert.True(result);
            _mockMongoRepo.Verify(r => r.DeleteAsync(candidaturaId), Times.Once);
            _mockOracleContext.Verify(c => c.Candidaturas.Remove(candidaturaParaDeletar), Times.Once);
            _mockOracleContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}