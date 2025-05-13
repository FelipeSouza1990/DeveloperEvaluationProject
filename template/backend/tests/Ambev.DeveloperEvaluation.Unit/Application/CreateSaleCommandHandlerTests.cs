using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleCommandHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly CreateSaleCommandHandler _handler;
        private readonly Mock<IMediator> _mediatorMock;

        public CreateSaleCommandHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _mediatorMock = new Mock<IMediator>(); // novo mock
            _handler = new CreateSaleCommandHandler(_repositoryMock.Object, _mediatorMock.Object); // passando os dois
        }

        [Fact]
        public async Task Handle_Should_Save_Sale_When_Command_Is_Valid()
        {
            // Arrange
            var command = new CreateSaleCommand
            {
                Items = new List<CreateSaleItemCommand>
            {
                new() { ProductName = "Cerveja", Quantity = 2, UnitPrice = 5.99m },
                new() { ProductName = "Refrigerante", Quantity = 1, UnitPrice = 3.50m }
            }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty(); // GUID gerado
            _repositoryMock.Verify(r => r.AddAsync(It.Is<Sale>(s =>
                s.Items.Count == 2 &&
                s.Items.Any(i => i.ProductName == "Cerveja") &&
                s.Items.Any(i => i.Quantity == 1)
            )), Times.Once);
        }
    }
}
