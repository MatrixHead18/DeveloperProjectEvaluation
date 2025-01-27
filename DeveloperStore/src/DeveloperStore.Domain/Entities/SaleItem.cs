namespace DeveloperStore.Domain.Entities
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }

        public SaleItem(string productName, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TotalAmount = totalAmount;
        }
    }
}
