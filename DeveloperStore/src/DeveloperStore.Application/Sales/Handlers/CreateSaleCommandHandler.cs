using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Interfaces;
using MediatR;

namespace DeveloperStore.Application.Sales.Handlers
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleService _saleService;
        private readonly IMediator _mediator;

        public CreateSaleCommandHandler(ISaleService saleService, IMediator mediator)
        {
            _mediator = mediator;
            _saleService = saleService;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var saleId = await _saleService
                .CreateSaleAsync(
                    request.SaleNumber, 
                    request.Customer, 
                    request.Branch, 
                    request.SaleDate, 
                    cancellationToken
                );

            return await Task.FromResult(saleId);
        }
    }
}