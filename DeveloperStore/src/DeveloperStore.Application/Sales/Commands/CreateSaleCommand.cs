using MediatR;

namespace DeveloperStore.Application.Sales.Commands
{
    public class CreateSaleCommand : IRequest<Guid>
    {
        public string SaleNumber { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
