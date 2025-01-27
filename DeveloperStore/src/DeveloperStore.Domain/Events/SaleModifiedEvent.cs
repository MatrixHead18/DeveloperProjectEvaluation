using MediatR;

namespace DeveloperStore.Domain.Events
{
    public class SaleModifiedEvent : INotification
    {
        public Guid SaleId { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public SaleModifiedEvent(Guid saleId, DateTime modifiedEvent)
        {
            SaleId = saleId;
            ModifiedDate = modifiedEvent;
        }
    }
}
