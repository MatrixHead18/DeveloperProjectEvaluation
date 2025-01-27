using DeveloperStore.Domain.Events;
using MediatR;

namespace DeveloperStore.Application.EventHandlers
{
    public class ItemCancelledEventHandler : INotificationHandler<ItemCancelledEvent>
    {
        public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[Event: ItemCancelled] SaleId: {notification.SaleId}, Product: {notification.ProductName}, Timestamp: {DateTime.UtcNow}");
            return Task.CompletedTask;
        }
    }
}