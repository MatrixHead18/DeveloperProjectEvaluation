using MediatR;

namespace DeveloperStore.Application.Sales.Commands
{
    public class AddItemCommand : IRequest
    {
        public Guid SaleId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
