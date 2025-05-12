using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.GetSale
{
    public class GetSaleByIdQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly GetSaleByIdQueryHandler _handler;

        public GetSaleByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _handler = new GetSaleByIdQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Sale_When_It_Exists()
        {
            // Arrange
            var saleId = Guid.NewGuid();

            var sale = new Sale("S123", Guid.NewGuid(), "João", Guid.NewGuid(), "Filial 1");
            sale.AddItem(Guid.NewGuid(), "Produto A", 2, 10);
            sale.AddItem(Guid.NewGuid(), "Produto B", 1, 20);

            typeof(Sale).GetProperty(nameof(Sale.Id))!.SetValue(sale, saleId);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(saleId))
                .ReturnsAsync(sale);

            var query = new GetSaleByIdQuery(saleId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result!.SaleNumber.Should().Be("S123");
            result.Items.Should().HaveCount(2);
            result.TotalAmount.Should().Be(40);
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_Sale_Does_Not_Exist()
        {
            // Arrange
            var query = new GetSaleByIdQuery(Guid.NewGuid());

            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Sale?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
