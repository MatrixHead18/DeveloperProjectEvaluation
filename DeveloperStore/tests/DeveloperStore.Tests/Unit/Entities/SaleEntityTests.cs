using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Events;
using MediatR;
using NSubstitute;

namespace DeveloperStore.Tests.Unit.Entities
{
    public class SaleEntityTests
    {
        [Fact]
        public void Sale_ShouldBeCreated_WithValidProperties()
        {
            // Arrange
            var saleNumber = "S001";
            var customer = "Customer1";
            var branch = "Branch1";
            var saleDate = DateTime.UtcNow;

            // Act
            var sale = new Sale(saleNumber, customer, branch, saleDate);

            // Assert
            Assert.Equal(saleNumber, sale.SaleNumber);
            Assert.Equal(customer, sale.Customer);
            Assert.Equal(branch, sale.Branch);
            Assert.Equal(saleDate, sale.SaleDate);
            Assert.Equal(0, sale.TotalAmount);
            Assert.False(sale.IsCancelled);
        }

        [Fact]
        public void UpdateSale_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            var newSaleDate = DateTime.UtcNow.AddDays(1);
            var newBranch = "Branch2";
            var newCustomer = "Customer2";

            // Act
            sale.UpdateSale(newSaleDate, newBranch, newCustomer);

            // Assert
            Assert.Equal(newSaleDate, sale.SaleDate);
            Assert.Equal(newBranch, sale.Branch);
            Assert.Equal(newCustomer, sale.Customer);
        }

        [Fact]
        public void AddItem_ShouldAddItemAndUpdateTotalAmount()
        {
            // Arrange
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            var productName = "Product1";
            var quantity = 5;
            var unitPrice = 10m;
            var mediator = Substitute.For<IMediator>();

            // Act
            sale.AddItem(productName, quantity, unitPrice, mediator);

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal(productName, sale.Items.First().ProductName);
            Assert.Equal(quantity, sale.Items.First().Quantity);
            Assert.Equal(unitPrice, sale.Items.First().UnitPrice);
            Assert.Equal(0.1m, sale.Items.First().Discount);
            Assert.Equal(quantity * unitPrice * 0.9m, sale.TotalAmount);
            mediator.Received(1).Publish(Arg.Any<SaleModifiedEvent>());
        }

        [Fact]
        public void AddItem_ShouldThrowException_WhenQuantityExceedsLimit()
        {
            // Arrange
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            var productName = "Product1";
            var quantity = 21; // Exceeds limit
            var unitPrice = 10m;
            var mediator = Substitute.For<IMediator>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() =>
                sale.AddItem(productName, quantity, unitPrice, mediator));
            Assert.Equal("Cannot sell more than 20 identical items.", exception.Message);
        }

        [Fact]
        public void Cancel_ShouldSetIsCancelledTrue()
        {
            // Arrange
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            var mediator = Substitute.For<IMediator>();

            // Act
            sale.Cancel(mediator);

            // Assert
            Assert.True(sale.IsCancelled);
            mediator.Received(1).Publish(Arg.Any<SaleCancelledEvent>());
        }

        [Fact]
        public void CancelItem_ShouldRemoveItemAndUpdateTotalAmount()
        {
            // Arrange
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            var productName = "Product1";
            var quantity = 5;
            var unitPrice = 10m;
            var mediator = Substitute.For<IMediator>();
            sale.AddItem(productName, quantity, unitPrice, mediator);
            var initialTotalAmount = sale.TotalAmount;

            // Act
            sale.CancelItem(productName, mediator);

            // Assert
            Assert.Empty(sale.Items);
            Assert.Equal(initialTotalAmount - (quantity * unitPrice * 0.9m), sale.TotalAmount); // Amount after removing item
            mediator.Received(1).Publish(Arg.Any<ItemCancelledEvent>());
        }

        [Fact]
        public void CancelItem_ShouldThrowException_WhenItemNotFound()
        {
            // Arrange
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            var mediator = Substitute.For<IMediator>();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => sale.CancelItem("NonExistingProduct", mediator));
            Assert.Equal("Item not found in the sale.", exception.Message);
        }
    }

}