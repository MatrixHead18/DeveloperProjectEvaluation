using DeveloperStore.Application.Sales.Commands;
using FluentValidation;

namespace DeveloperStore.WebApi.Features.Sales.Validators
{
    public class AddSaleItemValidator : AbstractValidator<AddItemCommand>
    {
        public AddSaleItemValidator()
        {
            RuleFor(user => user.ProductName).NotEmpty().WithMessage("Product Name is required");
            RuleFor(user => user.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be greater than 0");
            RuleFor(user => user.SaleId).NotEmpty().WithMessage("Sale ID is required");
            RuleFor(user => user.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
