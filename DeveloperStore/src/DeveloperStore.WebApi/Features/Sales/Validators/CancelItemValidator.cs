using DeveloperStore.Application.Sales.Commands;
using FluentValidation;

namespace DeveloperStore.WebApi.Features.Sales.Validators
{
    public class CancelItemValidator : AbstractValidator<CancelItemCommand>
    {
        public CancelItemValidator()
        {
            RuleFor(user => user.ProductName).NotEmpty().WithMessage("Product Name is required");
            RuleFor(user => user.SaleId).NotEmpty().WithMessage("Sale ID is required");
        }
    }
}
