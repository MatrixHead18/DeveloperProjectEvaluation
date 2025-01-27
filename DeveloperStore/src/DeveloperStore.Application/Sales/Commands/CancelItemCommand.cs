using MediatR;

namespace DeveloperStore.Application.Sales.Commands
{
    public class CancelItemCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }
        public string ProductName { get; set; }
    }

}
