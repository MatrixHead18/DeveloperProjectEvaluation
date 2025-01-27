using DeveloperStore.Application.Sales.Interfaces;
using DeveloperStore.Application.Sales.Queries;
using DeveloperStore.Domain.Entities;
using MediatR;

namespace DeveloperStore.Application.Sales.Handlers
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, IEnumerable<Sale>>
    {
        private readonly ISaleService _saleService;

        public GetAllSalesQueryHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<IEnumerable<Sale>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            return await _saleService.GetAllSalesAsync(cancellationToken);
        }
    }
}
