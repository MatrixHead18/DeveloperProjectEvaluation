using DeveloperStore.Application.Sales.Commands;
using DeveloperStore.Application.Sales.Queries;
using DeveloperStore.Domain.Entities;
using DeveloperStore.WebApi.Common;
using DeveloperStore.WebApi.Features.Sales.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.WebApi.Features.Sales.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var saleId = await _mediator.Send(command);
            return Created(nameof(GetSaleById), new { saleId }, saleId);
        }

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetSaleById(Guid saleId)
        {
            var query = new GetSaleByIdQuery { SaleId = saleId };

            var validator = new GetSaleByIdValidator();
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var sale = await _mediator.Send(query);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesPaginated()
        {
            var query = new GetAllSalesQuery();
            var sales = await _mediator.Send(query);
            if (sales == null || !sales.Any())
                return NotFound("There's no sales stored in database");

            return OkPaginated(await PaginatedList<Sale>.CreateAsync(sales.AsQueryable(), 1, 10));
        }

        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateSale(Guid saleId, [FromBody] UpdateSaleCommand command)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (saleId != command.Id)
                return BadRequest("Sale ID mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{saleId}/items")]
        public async Task<IActionResult> AddItem(Guid saleId, [FromBody] AddItemCommand command)
        {
            var validator = new AddSaleItemValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (saleId != command.SaleId)
                return BadRequest("Sale ID mismatch.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{saleId}/items/{productName}")]
        public async Task<IActionResult> CancelItem(Guid saleId, string productName)
        {
            var command = new CancelItemCommand { SaleId = saleId, ProductName = productName };
            var validator = new CancelItemValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); ;

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{saleId}/cancel")]
        public async Task<IActionResult> CancelSale(Guid saleId)
        {
            var command = new CancelSaleCommand { SaleId = saleId };

            var validator = new CancelSaleValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); ;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
