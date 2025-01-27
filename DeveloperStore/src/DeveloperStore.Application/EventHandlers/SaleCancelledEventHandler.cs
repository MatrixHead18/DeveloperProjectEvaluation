using DeveloperStore.Domain.Events;
using MediatR;

namespace DeveloperStore.Application.EventHandlers
{
    public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
    {
        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Event: SaleCancelled] SaleId: {notification.SaleId}, Timestamp: {DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}
