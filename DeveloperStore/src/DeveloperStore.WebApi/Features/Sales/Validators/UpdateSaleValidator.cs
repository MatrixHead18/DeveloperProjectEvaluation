using DeveloperStore.Application.Sales.Commands;
using FluentValidation;

namespace DeveloperStore.WebApi.Features.Sales.Validators
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(user => user.Id).NotEmpty().WithMessage("ID is required");
            RuleFor(user => user.Customer).NotEmpty().WithMessage("Customer is required");
            RuleFor(user => user.Branch).NotEmpty().WithMessage("Branch is required");
        }
    }
}
