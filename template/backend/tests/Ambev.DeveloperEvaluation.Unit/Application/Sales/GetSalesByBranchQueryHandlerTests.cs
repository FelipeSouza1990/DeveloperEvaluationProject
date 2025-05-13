using Ambev.DeveloperEvaluation.Application.Sales.GetSalesByBranch;
using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSalesByBranchQueryHandlerTests
{
    private readonly Mock<ISaleRepository> _repositoryMock;
    private readonly GetSalesByBranchQueryHandler _handler;

    public GetSalesByBranchQueryHandlerTests()
    {
        _repositoryMock = new Mock<ISaleRepository>();
        _handler = new GetSalesByBranchQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Sales_By_Branch()
    {
        // Arrange
        var branchId = Guid.NewGuid();

        var sale1 = new Sale("S001", Guid.NewGuid(), "Cliente 1", branchId, "Filial 1");
        sale1.AddItem(Guid.NewGuid(), "Produto 1", 2, 10.0m);

        var sale2 = new Sale("S002", Guid.NewGuid(), "Cliente 2", branchId, "Filial 1");
        sale2.AddItem(Guid.NewGuid(), "Produto 2", 1, 15.0m);

        var unrelatedSale = new Sale("S003", Guid.NewGuid(), "Cliente 3", Guid.NewGuid(), "Outra Filial");

        _repositoryMock.Setup(r => r.GetAllAsync())
                       .ReturnsAsync(new List<Sale> { sale1, sale2, unrelatedSale });

        var query = new GetSalesByBranchQuery(branchId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.All(r => r.BranchId == branchId).Should().BeTrue();
    }
}
