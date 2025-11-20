using IncludIA.Application.Service;
using IncludIA.Domain.Entities;
using IncludIA.Domain.Interfaces;
using Moq;
using Xunit;

namespace IncludIA.Tests
{
    public class CoreServicesTests
    {
        [Fact]
        public async Task RecruiterService_Create_DeveChamarRepository()
        {
            var repoMock = new Mock<IRecruiterRepository>();
            var service = new RecruiterService(repoMock.Object);
            var recruiter = new Recruiter { Id = Guid.NewGuid(), Nome = "RH Tech" };

            await service.CreateAsync(recruiter);

            repoMock.Verify(r => r.AddAsync(recruiter), Times.Once);
        }

        [Fact]
        public async Task EmpresaService_GetAll_DeveRetornarLista()
        {
            var repoMock = new Mock<IEmpresaRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Empresa> { new Empresa() });
            var service = new EmpresaService(repoMock.Object);

            var result = await service.GetAllAsync();

            Assert.NotEmpty(result);
        }
        
        [Fact]
        public async Task NotificationService_GetUserNotifications_DeveFiltrarPorId()
        {
            var userId = Guid.NewGuid();
            var repoMock = new Mock<INotificationRepository>();
            var service = new NotificationService(repoMock.Object, null!); 

            repoMock.Setup(r => r.GetByUserIdAsync(userId))
                .ReturnsAsync(new List<Notification> { new Notification { Mensagem = "Olá" } });

            var result = await service.GetUserNotificationsAsync(userId);

            Assert.Single(result);
        }
    }
}