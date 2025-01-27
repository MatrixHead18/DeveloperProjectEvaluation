using DeveloperStore.Application.EventHandlers;
using DeveloperStore.Domain.Events;
using FluentAssertions;

namespace DeveloperStore.Tests.Integration.EventHandlers
{
    public class SaleCreatedEventHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldLogEvent_WhenSaleIsCreated()
        {
            // Arrange
            var saleCreatedEvent = new SaleCreatedEvent(
                Guid.NewGuid(),
                "someCustomer",
                100.0m
            );

            var handler = new SaleCreatedEventHandler();

            // Act
            var exception = await Record.ExceptionAsync(() => handler.Handle(saleCreatedEvent, default));

            // Assert
            exception.Should().BeNull();
        }
    }
}
