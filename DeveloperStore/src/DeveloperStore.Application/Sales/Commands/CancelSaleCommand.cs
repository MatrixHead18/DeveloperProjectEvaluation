using MediatR;

namespace DeveloperStore.Application.Sales.Commands
{
    public class CancelSaleCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }
    }
}
