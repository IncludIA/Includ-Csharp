using IncludIA.Application.Service;
using IncludIA.Domain.Enums;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;
using Moq;
using Xunit;
using MatchEntity = IncludIA.Domain.Entities.Match; 

namespace IncludIA.Tests
{
    public class MatchServiceTests
    {
        private readonly Mock<IMatchRepository> _repoMock;
        private readonly MatchService _service;
        
        public MatchServiceTests()
        {
            _repoMock = new Mock<IMatchRepository>();
            _service = new MatchService(_repoMock.Object, null!); 
        }

        [Fact]
        public void UpdateStatus_DeveDarMatch_QuandoAmbosDeremLike()
        {
            var match = new MatchEntity 
            { 
                IsLikedByCandidate = true, 
                IsLikedByRecruiter = true,
                Status = MatchStatus.Pendente 
            };

            match.UpdateStatus(); 

            Assert.Equal(MatchStatus.Matched, match.Status);
        }

        [Fact]
        public void UpdateStatus_DeveRejeitar_QuandoCandidatoNaoDerLike()
        {
            var match = new MatchEntity 
            { 
                IsLikedByCandidate = false, 
                IsLikedByRecruiter = true 
            };
            
            match.UpdateStatus();
            
            Assert.Equal(MatchStatus.RejeitadoCandidato, match.Status);
        }
        
        [Fact]
        public async Task CreateMatchAsync_DeveChamarRepositorio()
        {
            var match = new MatchEntity { Id = Guid.NewGuid(), Status = MatchStatus.Pendente };
            
            try { await _service.CreateMatchAsync(match); } catch {}
            
            _repoMock.Verify(r => r.AddAsync(match), Times.Once);
        }
    }
}