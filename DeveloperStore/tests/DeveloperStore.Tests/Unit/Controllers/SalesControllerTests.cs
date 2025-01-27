using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Queries;
using DeveloperStore.Domain.Entities;
using DeveloperStore.WebApi.Features.Sales.Controllers.v1;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace DeveloperStore.Tests.Unit.Controllers
{
    public class SalesControllerTests
    {
        private readonly IMediator _mediator;
        private readonly SalesController _controller;

        public SalesControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new SalesController(_mediator);
        }

        [Fact]
        public async Task CreateSale_ShouldReturnCreatedResult_WithSaleId()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = Guid.NewGuid().ToString(),
                Branch = "Test",
                SaleDate = DateTime.UtcNow,
                SaleNumber = "123456",
            };
            var saleId = Guid.NewGuid();
            _mediator.Send(command).Returns(saleId);

            // Act
            var result = await _controller.CreateSale(command);

            // Assert
            var createdResult = result as CreatedAtRouteResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
            createdResult.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSaleById_ShouldReturnNotFound_WhenSaleDoesNotExist()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            _mediator.Send(Arg.Any<GetSaleByIdQuery>()).Returns((Sale)null);

            // Act
            var result = await _controller.GetSaleById(saleId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
