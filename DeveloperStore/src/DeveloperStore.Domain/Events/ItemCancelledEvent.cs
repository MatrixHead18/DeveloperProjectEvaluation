using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class ItemCancelledEvent : INotification
    {
        public Guid SaleId { get; private set; }
        public string ProductName { get; private set; }

        public ItemCancelledEvent(Guid saleId, string productName)
        {
            SaleId = saleId;
            ProductName = productName;
        }
    }
}
