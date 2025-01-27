using DeveloperStore.Domain.Events;
using MediatR;

namespace DeveloperStore.Application.EventHandlers
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"[Event: SaleCreated] SaleId: {notification.SaleId}, " +
                $"CustomerId: {notification.Customer}, " +
                $"Total: {notification.TotalAmount}, " +
                $"Timestamp: {DateTime.UtcNow}"
            );

            return Task.CompletedTask;
        }
    }
}
