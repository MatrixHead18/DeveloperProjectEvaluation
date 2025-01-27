using DeveloperStore.Application.Sales.Interfaces;
using DeveloperStore.Application.Sales.Queries;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using MediatR;

namespace DeveloperStore.Application.Sales.Handlers
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Sale>
    {
        private readonly ISaleService _saleService;

        public GetSaleByIdQueryHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<Sale> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleService.GetSaleByIdAsync(request.SaleId, cancellationToken);
            return sale;
        }
    }
}
