using IncludIA.Domain.Entities;
using IncludIA.Domain.Documents;
using IncludIA.Domain.Interfaces;
using IncludIA.Infrastructure.Context;

namespace IncludIA.Application.Service
{
    public class NotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly MongoDbContext _mongoContext;

        public NotificationService(INotificationRepository repository, MongoDbContext mongoContext)
        {
            _repository = repository;
            _mongoContext = mongoContext;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(Guid userId)
        {
            return await _repository.GetByUserIdAsync(userId);
        }

        public async Task SendNotificationAsync(Notification notification)
        {
            notification.CreatedAt = DateTime.UtcNow;
            notification.IsRead = false;
            await _repository.AddAsync(notification);
            var notifDoc = new NotificationDocument
            {
                Id = notification.Id,
                Mensagem = notification.Mensagem,
                Tipo = notification.Tipo.ToString(),
                UserId = notification.CandidateId ?? notification.RecruiterId,
                IsRead = false,
                CreatedAt = notification.CreatedAt
            };
            await _mongoContext.Notifications.InsertOneAsync(notifDoc);
        }

        public async Task MarkAsReadAsync(Guid id)
        {
            await _repository.MarkAsReadAsync(id);
            var filter = MongoDB.Driver.Builders<NotificationDocument>.Filter.Eq(x => x.Id, id);
            var update = MongoDB.Driver.Builders<NotificationDocument>.Update.Set(x => x.IsRead, true);
            await _mongoContext.Notifications.UpdateOneAsync(filter, update);
        }
    }
}