using DeveloperStore.Domain.Events;
using MediatR;

namespace DeveloperStore.Application.EventHandlers
{
    public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
    {
        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Event: SaleModified] SaleId: {notification.SaleId}, ModifiedDate: {notification.ModifiedDate}, Timestamp: {DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}
