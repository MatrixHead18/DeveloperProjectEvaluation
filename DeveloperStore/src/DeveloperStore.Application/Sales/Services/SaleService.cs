using DeveloperStore.Application.Sales.Interfaces;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Events;
using DeveloperStore.Domain.Repositories;
using MediatR;

namespace DeveloperStore.Application.Sales.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        public SaleService(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<Guid> CreateSaleAsync(string saleNumber, string customer, string branch, DateTime saleDate, CancellationToken cancellationToken)
        {
            var sale = new Sale(saleNumber, customer, branch, saleDate);
            var isSuccess = await _saleRepository.AddSaleAsync(sale, cancellationToken);
            if (isSuccess > 0)
            {
                await _mediator.Publish(new SaleCreatedEvent(sale.Id, sale.Customer, sale.TotalAmount), cancellationToken);
                return sale.Id;
            }

            throw new InvalidOperationException("An error occur on adding sale in database.");
        }

        public async Task<Sale> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _saleRepository.GetSaleByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync(CancellationToken cancellationToken)
        {
            return await _saleRepository.GetAllSalesAsync(cancellationToken);
        }

        public async Task AddItem(Guid saleId, string productName, int quantity, decimal unitPrice)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleId);

            sale.AddItem(productName, quantity, unitPrice, _mediator);

            await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task<int> CancelSale(Guid saleId)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleId);

            sale.Cancel(_mediator);

            return await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task<int> CancelItem(Guid saleId, string productName)
        {
            var sale = await _saleRepository.GetSaleByIdAsync(saleId);

            sale.CancelItem(productName, _mediator);

            return await _saleRepository.UpdateSaleAsync(sale);
        }

        public async Task<Sale> UpdateBasicSaleInfoAsync(Guid saleId, string customer, string branch, DateTime saleDate, CancellationToken cancellationToken)
        {
            var saleToUpdate = await _saleRepository.GetSaleByIdAsync(saleId);

            saleToUpdate.UpdateSale(saleDate, branch, customer);

            await _saleRepository.UpdateSaleAsync(saleToUpdate, cancellationToken);

            return saleToUpdate;
        }
    }
}
