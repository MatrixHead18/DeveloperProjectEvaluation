using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class SaleCreatedEvent : INotification
    {
        public Guid SaleId { get; private set; }
        public string Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public SaleCreatedEvent(Guid saleId, string customer, decimal totalAmount)
        {
            SaleId = saleId;
            Customer = customer;
            TotalAmount = totalAmount;
        }
    }
}
