using Ambev.DeveloperEvaluation.Application.Sales.GetCancelledSales;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetCancelledSalesQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly GetCancelledSalesQueryHandler _handler;

        public GetCancelledSalesQueryHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _handler = new GetCancelledSalesQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Only_Cancelled_Sales()
        {
            // Arrange
            var sale1 = new Sale("S001", Guid.NewGuid(), "Cliente 1", Guid.NewGuid(), "Filial 1");
            var sale2 = new Sale("S002", Guid.NewGuid(), "Cliente 2", Guid.NewGuid(), "Filial 2");
            sale2.Cancel();

            var sales = new List<Sale> { sale1, sale2 };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(sales);

            // Act
            var result = await _handler.Handle(new GetCancelledSalesQuery(), CancellationToken.None);

            // Assert
            result.Should().HaveCount(1);
            result[0].SaleNumber.Should().Be("S002");
        }
    }
}
