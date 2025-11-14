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
        private readonly Mock<DbSet<Candidato>> _mockCandidatoDbSet;
        private readonly CandidatoService _service;

        public CandidatoServiceTests()
        {
            _mockMongoRepo = new Mock<ICandidatoRepository>();
            var options = new DbContextOptions<OracleDbContext>();
            _mockOracleContext = new Mock<OracleDbContext>(options);
            _mockCandidatoDbSet = new Mock<DbSet<Candidato>>();
            _mockOracleContext.Setup(c => c.Candidatos).Returns(_mockCandidatoDbSet.Object);
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

        [Fact]
        public async Task UpdateAsync_DeveAtualizar_EmAmbosOsBancos()
        {
            var candidato = new Candidato { Id = "123", Nome = "Nome Atualizado" };
            _mockMongoRepo.Setup(r => r.UpdateAsync(candidato)).ReturnsAsync(true);
            _mockOracleContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _service.UpdateAsync("123", candidato);
            Assert.True(result);
            _mockMongoRepo.Verify(r => r.UpdateAsync(candidato), Times.Once);
            _mockOracleContext.Verify(c => c.Candidatos.Update(candidato), Times.Once);
            _mockOracleContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveDeletar_DeAmbosOsBancos()
        {
            var candidatoId = "123";
            var candidatoParaDeletar = new Candidato { Id = candidatoId, Nome = "Para Deletar" };
            _mockMongoRepo.Setup(r => r.DeleteAsync(candidatoId)).ReturnsAsync(true);
            _mockCandidatoDbSet.Setup(s => s.FindAsync(candidatoId)).ReturnsAsync(candidatoParaDeletar);
            _mockOracleContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _service.DeleteAsync(candidatoId);
            Assert.True(result);
            _mockMongoRepo.Verify(r => r.DeleteAsync(candidatoId), Times.Once);
            _mockOracleContext.Verify(c => c.Candidatos.Remove(candidatoParaDeletar), Times.Once);
            _mockOracleContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}