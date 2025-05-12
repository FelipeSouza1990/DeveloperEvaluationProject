using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleCommandValidatorTests
{
    private readonly CreateSaleCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Items_Is_Empty()
    {
        var command = new CreateSaleCommand
        {
            Items = new List<CreateSaleItemCommand>()
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.Items);
    }

    [Fact]
    public void Should_Have_Error_When_Item_Is_Invalid()
    {
        var command = new CreateSaleCommand
        {
            Items = new List<CreateSaleItemCommand>
            {
                new() { ProductName = "", Quantity = 0, UnitPrice = 0 }
            }
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor("Items[0].ProductName");
        result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        result.ShouldHaveValidationErrorFor("Items[0].UnitPrice");
    }

    //[Fact]
    //public void Should_Not_Have_Errors_When_Command_Is_Valid()
    //{
    //    var command = new CreateSaleCommand
    //    {
    //        Items = new List<CreateSaleItemCommand>
    //        {
    //            new() { ProductName = "Guaraná", Quantity = 2, UnitPrice = 4.99m }
    //        }
    //    };

    //    var result = _validator.TestValidate(command);
    //    result.IsValid.Should().BeTrue();
    //}
}
