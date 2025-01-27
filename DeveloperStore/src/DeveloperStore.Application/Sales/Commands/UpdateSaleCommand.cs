using DeveloperStore.Domain.Entities;
using MediatR;

namespace DeveloperStore.Application.Sales.Commands
{
    public class UpdateSaleCommand : IRequest<Sale>
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
    }

}
