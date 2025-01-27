using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Application.Sales.Interfaces
{
    public interface ISaleService
    {
        Task<Guid> CreateSaleAsync(string saleNumber, string customer, string branch, DateTime saleDate, CancellationToken cancellationToken);
        Task<Sale> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Sale>> GetAllSalesAsync(CancellationToken cancellationToken);
        Task AddItem(Guid saleId, string productName, int quantity, decimal unitPrice);
        Task<int> CancelSale(Guid saleId);
        Task<int> CancelItem(Guid saleId, string productName);
        Task<Sale> UpdateBasicSaleInfoAsync(Guid saleId, string customer, string branch, DateTime saleDate, CancellationToken cancellationToken);
    }
}