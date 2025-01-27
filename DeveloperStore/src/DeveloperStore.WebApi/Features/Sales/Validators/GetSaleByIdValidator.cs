using DeveloperStore.Application.Sales.Queries;
using FluentValidation;

namespace DeveloperStore.WebApi.Features.Sales.Validators
{
    public class GetSaleByIdValidator : AbstractValidator<GetSaleByIdQuery>
    {
        public GetSaleByIdValidator()
        {
            RuleFor(x => x.SaleId)
                .NotEmpty()
                .WithMessage("Sale ID is required");
        }
    }
}
