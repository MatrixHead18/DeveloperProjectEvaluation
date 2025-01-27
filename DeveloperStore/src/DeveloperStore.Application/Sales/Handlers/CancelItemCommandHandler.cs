using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Interfaces;
using MediatR;

namespace DeveloperStore.Application.Sales.Handlers
{
    public class CancelItemCommandHandler : IRequestHandler<CancelItemCommand, Unit>
    {
        private readonly ISaleService _saleService;

        public CancelItemCommandHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<Unit> Handle(CancelItemCommand request, CancellationToken cancellationToken)
        {
            var isSucess = await _saleService.CancelItem(request.SaleId, request.ProductName);

            if (isSucess == 0)
                throw new InvalidOperationException("");

            return Unit.Value;
        }
    }
}