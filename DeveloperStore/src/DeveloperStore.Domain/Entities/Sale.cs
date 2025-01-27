using DeveloperStore.Domain.Events;
using MediatR;

namespace DeveloperStore.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Branch { get; private set; }
        public List<SaleItem> Items { get; private set; } = new();
        public bool IsCancelled { get; private set; }

        public Sale(string saleNumber, string customer, string branch, DateTime saleDate)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            Customer = customer;
            Branch = branch;
            SaleDate = saleDate;
            TotalAmount = 0;
            IsCancelled = false;
        }

        public Sale UpdateSale(DateTime saleDate, string branch, string customer) 
        {
            SaleDate = saleDate;
            Branch = branch;
            Customer = customer;

            return this;
        }

        public override bool Equals(object? obj)
        {
            return obj is Sale sale &&
                   Id.Equals(sale.Id) &&
                   SaleNumber == sale.SaleNumber &&
                   SaleDate == sale.SaleDate &&
                   Customer == sale.Customer &&
                   TotalAmount == sale.TotalAmount &&
                   Branch == sale.Branch &&
                   EqualityComparer<List<SaleItem>>.Default.Equals(Items, sale.Items) &&
                   IsCancelled == sale.IsCancelled;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, SaleNumber, SaleDate, Customer, TotalAmount, Branch, Items, IsCancelled);
        }

        public void AddItem(string productName, int quantity, decimal unitPrice, IMediator mediator)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            var discount = CalculateDiscount(quantity);
            var discountedPrice = unitPrice * (1 - discount);
            var totalItemAmount = discountedPrice * quantity;

            Items.Add(new SaleItem(productName, quantity, unitPrice, discount, totalItemAmount));

            TotalAmount += totalItemAmount;
            mediator.Publish(new SaleModifiedEvent(Id, DateTime.UtcNow));
        }

        public void Cancel(IMediator mediator)
        {
            IsCancelled = true;
            mediator.Publish(new SaleCancelledEvent(Id));
        }

        public void CancelItem(string productName, IMediator mediator)
        {
            var item = Items.FirstOrDefault(i => i.ProductName == productName);
            if (item == null)
                throw new InvalidOperationException("Item not found in the sale.");

            Items.Remove(item);
            TotalAmount -= item.TotalAmount;

            mediator.Publish(new ItemCancelledEvent(Id, productName));
        }

        private static decimal CalculateDiscount(int quantity)
        {
            return quantity switch
            {
                >= 10 and <= 20 => 0.2m,
                >= 4 and < 10 => 0.1m,
                _ => 0m
            };
        }
    }
}
