using DeveloperStore.Domain.Entities;
using MediatR;

namespace DeveloperStore.Application.Sales.Queries
{
    public class GetAllSalesQuery : IRequest<IEnumerable<Sale>> { }
}
