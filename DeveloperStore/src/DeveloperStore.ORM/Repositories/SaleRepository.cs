using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SaleDbContext _dbContext;
        public SaleRepository(SaleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddSaleAsync(Sale sale, CancellationToken cancellationToken)
        {
            _dbContext.Sales.Add(sale);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Sales.ToListAsync(cancellationToken);
        }

        public async Task<Sale> GetSaleByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.Sales
                .Include(s => s.Items)
                .FirstOrDefault(s => s.Id == id)) ?? throw new KeyNotFoundException("Sale not found.");
        }

        public async Task<int> UpdateSaleAsync(Sale sale, CancellationToken cancellationToken)
        {
            _dbContext.Sales.Update(sale);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
