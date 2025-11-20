using IncludIA.Application.Service;
using IncludIA.Domain.Documents;
using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace IncludIA.Tests
{
    public class JobVagaServiceTests
    {
        private readonly Mock<IJobVagaRepository> _oracleRepoMock;
        private readonly Mock<IMongoCollection<JobVagaDocument>> _mongoCollectionMock;
        private readonly JobVagaService _service;

        public JobVagaServiceTests()
        {
            _oracleRepoMock = new Mock<IJobVagaRepository>();
            _mongoCollectionMock = new Mock<IMongoCollection<JobVagaDocument>>();

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);
        }

        [Fact]
        public async Task CreateAsync_DeveSalvarNoOracle()
        {
            var vaga = new JobVaga { Id = Guid.NewGuid(), Titulo = "Dev C#", SalarioMax = 5000 };

            _oracleRepoMock.Setup(r => r.AddAsync(It.IsAny<JobVaga>())).Returns(Task.CompletedTask);
        }
        
        [Fact]
        public async Task GetAllAsync_DeveRetornarVagasDoOracle()
        {
            var service = new JobVagaService(_oracleRepoMock.Object, null!); 

            var listaVagas = new List<JobVaga> { new JobVaga { Titulo = "Dev Java" } };
            _oracleRepoMock.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(listaVagas);

            var result = await service.GetAllAsync(1, 10);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Dev Java", result.First().Titulo);
        }
    }
}