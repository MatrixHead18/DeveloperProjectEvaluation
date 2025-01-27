using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Interfaces;
using MediatR;

namespace DeveloperStore.Application.Sales.Handlers
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleService _saleService;

        public CancelSaleCommandHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var isSucess = await _saleService.CancelSale(request.SaleId);

            if (isSucess == 0)
                throw new InvalidOperationException("");

            return Unit.Value;
        }
    }
}