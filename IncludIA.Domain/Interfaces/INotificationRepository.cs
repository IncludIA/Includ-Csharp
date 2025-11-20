using IncludIA.Domain.Entities;

namespace IncludIA.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId); // Genérico para Candidato ou Recrutador
        Task AddAsync(Notification notification);
        Task MarkAsReadAsync(Guid id);
    }
}