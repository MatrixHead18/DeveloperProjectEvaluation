using DeveloperStore.Application.Sales.Commands;
using FluentValidation;

namespace DeveloperStore.WebApi.Features.Sales.Validators
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(user => user.SaleNumber).NotEmpty().WithMessage("Sale Number is required");
            RuleFor(user => user.Customer).NotEmpty().WithMessage("Customer is required");
            RuleFor(user => user.Branch).NotEmpty().WithMessage("Branch is required");
        }
    }
}