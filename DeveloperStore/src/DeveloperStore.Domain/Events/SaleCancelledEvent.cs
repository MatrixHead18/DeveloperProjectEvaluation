using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class SaleCancelledEvent : INotification
    {
        public Guid SaleId { get; private set; }
        public SaleCancelledEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
