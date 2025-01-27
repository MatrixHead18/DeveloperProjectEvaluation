using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Interfaces;
using DeveloperStore.Domain.Entities;
using MediatR;

namespace DeveloperStore.Application.Sales.Handlers
{
    public class UpdateBasicSaleInfoCommandHandler : IRequestHandler<UpdateSaleCommand, Sale>
    {
        private readonly ISaleService _saleService;
        private readonly IMediator _mediator;

        public UpdateBasicSaleInfoCommandHandler(ISaleService saleService, IMediator mediator)
        {
            _mediator = mediator;
            _saleService = saleService;
        }

        public async Task<Sale> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var saleUpdated = await _saleService
                .UpdateBasicSaleInfoAsync(
                    request.Id,
                    request.Customer,
                    request.Branch,
                    request.SaleDate,
                    cancellationToken
                );

            return saleUpdated;
        }
    }
}
