using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Interfaces;
using DeveloperStore.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using System.Text;
using System.Text.Json;

namespace DeveloperStore.Tests.Integration.Controllers
{
    public class SalesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ISaleService _saleService;

        public SalesControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _saleService = Substitute.For<ISaleService>();
        }

        [Fact]
        public async Task CreateSale_ShouldReturnCreatedStatus()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Customer = Guid.NewGuid().ToString(),
                Branch = "Test",
                SaleDate = DateTime.UtcNow,
                SaleNumber = "123456",
            };
            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            _saleService.CreateSaleAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<DateTime>(),
                Arg.Any<CancellationToken>()
            ).Returns(Guid.NewGuid());

            // Act
            var response = await _client.PostAsync("/api/v1/sales", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
