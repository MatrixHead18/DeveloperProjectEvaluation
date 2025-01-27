using DeveloperStore.Domain.Entities;

namespace DeveloperStore.Domain.Repositories
{
    public interface ISaleRepository
    { 
        Task<int> AddSaleAsync(Sale sale, CancellationToken cancellationToken);
        Task<Sale> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Sale>> GetAllSalesAsync(CancellationToken cancellationToken);
        Task<int> UpdateSaleAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}
