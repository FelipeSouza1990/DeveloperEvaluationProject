using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetAllSalesQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly GetAllSalesQueryHandler _handler;

        public GetAllSalesQueryHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _handler = new GetAllSalesQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_All_Sales()
        {
            // Arrange
            var sale1 = new Sale("S001", Guid.NewGuid(), "Cliente 1", Guid.NewGuid(), "Filial 1");
            sale1.AddItem(Guid.NewGuid(), "Produto 1", 2, 10.0m);

            var sale2 = new Sale("S002", Guid.NewGuid(), "Cliente 2", Guid.NewGuid(), "Filial 2");
            sale2.AddItem(Guid.NewGuid(), "Produto 2", 1, 15.0m);

            var sales = new List<Sale> { sale1, sale2 };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(sales);

            // Act
            var result = await _handler.Handle(new GetAllSalesQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result[0].SaleNumber.Should().Be("S001");
            result[1].SaleNumber.Should().Be("S002");
        }
    }
}
