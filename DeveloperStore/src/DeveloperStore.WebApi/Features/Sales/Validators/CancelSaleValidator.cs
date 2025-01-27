using DeveloperStore.Application.Sales.Commands;
using FluentValidation;

namespace DeveloperStore.WebApi.Features.Sales.Validators
{
    public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleValidator()
        {
            RuleFor(user => user.SaleId).NotEmpty().WithMessage("Sale ID is required");
        }
    }
}
