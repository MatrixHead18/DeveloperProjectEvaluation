using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeveloperStore.Application.Sales.Services;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Events;
using DeveloperStore.Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace DeveloperStore.Tests.Unit.Services
{
    public class SaleServiceTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mediator = Substitute.For<IMediator>();
            _saleService = new SaleService(_saleRepository, _mediator);
        }

        [Fact]
        public async Task CreateSaleAsync_ShouldCreateSaleAndReturnSaleId()
        {
            // Arrange
            var saleNumber = "S001";
            var customer = "Customer1";
            var branch = "Branch1";
            var saleDate = DateTime.UtcNow;
            var cancellationToken = CancellationToken.None;

            // Criação do objeto Sale
            var sale = new Sale(saleNumber, customer, branch, saleDate);

            // Configurar o repositório para retornar 1 indicando que a venda foi adicionada
            // Não precisamos definir o Id manualmente, ele será gerado automaticamente
            _saleRepository.AddSaleAsync(Arg.Any<Sale>(), cancellationToken).Returns(1);

            // Act
            var result = await _saleService.CreateSaleAsync(saleNumber, customer, branch, saleDate, cancellationToken);

            // Assert
            result.Should().NotBeEmpty();
            await _mediator.Received(1).Publish(Arg.Any<SaleCreatedEvent>(), cancellationToken);
            await _saleRepository.Received(1).AddSaleAsync(Arg.Any<Sale>(), cancellationToken);
        }



        [Fact]
        public async Task CreateSaleAsync_ShouldThrowException_WhenSaleNotAdded()
        {
            // Arrange
            var saleNumber = "S001";
            var customer = "Customer1";
            var branch = "Branch1";
            var saleDate = DateTime.UtcNow;
            var cancellationToken = CancellationToken.None;
            _saleRepository.AddSaleAsync(Arg.Any<Sale>(), cancellationToken).Returns(0);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _saleService.CreateSaleAsync(saleNumber, customer, branch, saleDate, cancellationToken));
            Assert.Equal("An error occur on adding sale in database.", exception.Message);
        }

        [Fact]
        public async Task GetSaleByIdAsync_ShouldReturnSale_WhenSaleExists()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            _saleRepository.GetSaleByIdAsync(saleId, Arg.Any<CancellationToken>()).Returns(sale);

            // Act
            var result = await _saleService.GetSaleByIdAsync(saleId, CancellationToken.None);

            // Assert
            Assert.Equal(sale, result);
        }

        [Fact]
        public async Task GetAllSalesAsync_ShouldReturnSales()
        {
            // Arrange
            var sales = new List<Sale>
        {
            new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow),
            new Sale("S002", "Customer2", "Branch2", DateTime.UtcNow)
        };
            _saleRepository.GetAllSalesAsync(Arg.Any<CancellationToken>()).Returns(sales);

            // Act
            var result = await _saleService.GetAllSalesAsync(CancellationToken.None);

            // Assert
            Assert.Equal(sales, result);
        }

        [Fact]
        public async Task AddItem_ShouldAddItemToSaleAndUpdate()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var productName = "Product1";
            var quantity = 5;
            var unitPrice = 10m;
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            _saleRepository.GetSaleByIdAsync(saleId).Returns(sale);

            // Act
            await _saleService.AddItem(saleId, productName, quantity, unitPrice);

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal(productName, sale.Items[0].ProductName);
            Assert.Equal(quantity, sale.Items[0].Quantity);
            await _saleRepository.Received(1).UpdateSaleAsync(sale);
        }

        [Fact]
        public async Task CancelSale_ShouldCancelSaleAndUpdate()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            _saleRepository.GetSaleByIdAsync(saleId).Returns(sale);

            // Act
            await _saleService.CancelSale(saleId);

            // Assert
            Assert.True(sale.IsCancelled);
            await _saleRepository.Received(1).UpdateSaleAsync(sale);
            await _mediator.Received(1).Publish(Arg.Any<SaleCancelledEvent>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task CancelItem_ShouldCancelItemAndUpdate()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var productName = "Product1";
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            sale.AddItem(productName, 5, 10m, _mediator);
            _saleRepository.GetSaleByIdAsync(saleId).Returns(sale);

            // Act
            await _saleService.CancelItem(saleId, productName);

            // Assert
            Assert.Empty(sale.Items);
            await _saleRepository.Received(1).UpdateSaleAsync(sale);
            await _mediator.Received(1).Publish(Arg.Any<ItemCancelledEvent>(), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task UpdateBasicSaleInfoAsync_ShouldUpdateSaleInfo()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var customer = "NewCustomer";
            var branch = "NewBranch";
            var saleDate = DateTime.UtcNow.AddDays(1);
            var sale = new Sale("S001", "Customer1", "Branch1", DateTime.UtcNow);
            _saleRepository.GetSaleByIdAsync(saleId).Returns(sale);

            // Act
            var result = await _saleService.UpdateBasicSaleInfoAsync(saleId, customer, branch, saleDate, CancellationToken.None);

            // Assert
            Assert.Equal(customer, result.Customer);
            Assert.Equal(branch, result.Branch);
            Assert.Equal(saleDate, result.SaleDate);
            await _saleRepository.Received(1).UpdateSaleAsync(sale, Arg.Any<CancellationToken>());
        }
    }
}