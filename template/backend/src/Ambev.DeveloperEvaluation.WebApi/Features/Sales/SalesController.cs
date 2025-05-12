using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSalesByBranch;
using Ambev.DeveloperEvaluation.Application.Sales.GetSalesByCustomer;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Sales.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("sales")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, ISaleRepository repository, IMapper mapper)
    {
        _mediator = mediator;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sale = await _repository.GetByIdAsync(id);
        if (sale == null)
            return NotFound();

        var dto = _mapper.Map<SaleDto>(sale);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<SaleDto>>(sales);
        return Ok(sales);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetByCustomerId(Guid customerId)
    {
        var result = await _mediator.Send(new GetSalesByCustomerQuery { CustomerId = customerId });
        return Ok(result);
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var success = await _mediator.Send(new CancelSaleCommand { SaleId = id });
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpGet("branch/{branchId:guid}")]
    public async Task<IActionResult> GetByBranch(Guid branchId)
    {
        var result = await _mediator.Send(new GetSalesByBranchQuery(branchId));
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command)
    {
        if (id != command.SaleId)
            return BadRequest("ID mismatch");

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSaleCommand command)
    {
        if (id != command.SaleId)
            return BadRequest("Id in route does not match command");

        await _mediator.Send(command);
        return NoContent();

    }


}