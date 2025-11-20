using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using Moq;
using Xunit;

namespace IncludIA.Tests
{
    public class CoreCrudTests
    {
        [Fact]
        public async Task EducationService_Add_DeveSalvar()
        {
            var repoMock = new Mock<IEducationRepository>();
            var service = new EducationService(repoMock.Object);
            var education = new Education { NomeInstituicao = "FIAP" };

            await service.AddEducationAsync(education);

            repoMock.Verify(r => r.AddAsync(education), Times.Once);
        }

        [Fact]
        public async Task ExperienceService_Add_DeveSalvar()
        {
            var repoMock = new Mock<IExperienceRepository>();
            var service = new ExperienceService(repoMock.Object);
            var xp = new Experience { TituloCargo = "Dev Pleno" };

            await service.AddExperienceAsync(xp);

            repoMock.Verify(r => r.AddAsync(xp), Times.Once);
        }

        [Fact]
        public async Task RecruiterService_Create_DeveSalvar()
        {
            var repoMock = new Mock<IRecruiterRepository>();
            var service = new RecruiterService(repoMock.Object);
            var recruiter = new Recruiter { Nome = "Ana RH" };

            await service.CreateAsync(recruiter);

            repoMock.Verify(r => r.AddAsync(recruiter), Times.Once);
        }

        [Fact]
        public async Task SavedJobService_Save_DeveSalvar()
        {
            var repoMock = new Mock<ISavedJobRepository>();
            var service = new SavedJobService(repoMock.Object);
            var userId = Guid.NewGuid();
            var vagaId = Guid.NewGuid();

            await service.SaveJobAsync(userId, vagaId);

            repoMock.Verify(r => r.AddAsync(It.IsAny<SavedJob>()), Times.Once);
        }
    }
}