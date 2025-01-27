using DeveloperStore.Domain.Entities;
using MediatR;

namespace DeveloperStore.Application.Sales.Queries
{
    public class GetSaleByIdQuery : IRequest<Sale>
    {
        public Guid SaleId { get; set; }
    }
}
