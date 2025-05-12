using Ambev.DeveloperEvaluation.Application.Sales.GetSalesByCustomer;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSalesByCustomerQueryHandlerTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly GetSalesByCustomerQueryHandler _handler;

        public GetSalesByCustomerQueryHandlerTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _handler = new GetSalesByCustomerQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Sales_For_Specific_Customer()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var sale1 = new Sale("S001", customerId, "Cliente X", Guid.NewGuid(), "Filial 1");
            sale1.AddItem(Guid.NewGuid(), "Produto 1", 2, 10.0m);

            var sale2 = new Sale("S002", customerId, "Cliente X", Guid.NewGuid(), "Filial 2");
            sale2.AddItem(Guid.NewGuid(), "Produto 2", 1, 15.0m);

            var sales = new List<Sale> { sale1, sale2 };

            _repositoryMock.Setup(r => r.GetByCustomerIdAsync(customerId)).ReturnsAsync(sales);

            var query = new GetSalesByCustomerQuery(customerId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.All(s => s.CustomerId == customerId).Should().BeTrue();
        }
    }
}
