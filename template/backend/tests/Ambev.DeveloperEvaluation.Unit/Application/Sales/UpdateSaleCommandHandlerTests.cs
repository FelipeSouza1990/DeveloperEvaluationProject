using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using Moq;
using Xunit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class UpdateSaleCommandHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly UpdateSaleCommandHandler _handler;

        public UpdateSaleCommandHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _handler = new UpdateSaleCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Update_Sale_When_Valid_Command()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var existingSale = new Sale("V001", Guid.NewGuid(), "Cliente Antigo", Guid.NewGuid(), "Filial Antiga");
            existingSale.AddItem(Guid.NewGuid(), "Produto Antigo", 2, 10m);

            _repositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(existingSale);

            var command = new UpdateSaleCommand
            {
                SaleId = saleId,
                SaleNumber = "V002",
                CustomerId = Guid.NewGuid(),
                CustomerName = "Novo Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Nova Filial",
                Items = new List<UpdateSaleItemDto>
                {
                    new() { ProductId = Guid.NewGuid(), ProductName = "Novo Produto", Quantity = 4, UnitPrice = 8.50m }
                }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(MediatR.Unit.Value);
            _repositoryMock.Verify(r => r.GetByIdAsync(saleId), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Sale_Not_Found()
        {
            // Arrange
            var command = new UpdateSaleCommand
            {
                SaleId = Guid.NewGuid(),
                SaleNumber = "V002",
                CustomerId = Guid.NewGuid(),
                CustomerName = "Novo Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Nova Filial",
                Items = new List<UpdateSaleItemDto>()
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}
