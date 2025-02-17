using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseCreateExpenseJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Create(
    [FromServices] ICreateExpenseUseCase useCase,
    [FromBody] RequestExpenseJson request)
  {
    // Utilizando o Exception filter, dispensa o uso de Try/Catch
    var response = await useCase.Execute(request);

    return Created(string.Empty, response);
  }

  [HttpGet]
  [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> GetAll([FromServices] IGetAllExpenseUseCase useCase)
  {
    var response = await useCase.Execute();

    if (response.Expenses.Count != 0)
    {
      return Ok(response);
    }

    return NoContent();
  }

  [HttpGet]
  [Route("{id}")]
  [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetById(
    [FromServices] IGetByIdExpenseUseCase useCase,
    [FromRoute] long id)
  {
    var response = await useCase.Execute(id);

    return Ok(response);
  }

  [HttpDelete]
  [Route("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(
    [FromServices] IDeleteExpenseUseCase useCase,
    [FromRoute] long id)
  {
    await useCase.Execute(id);

    return NoContent();
  }

  [HttpPut]
  [Route("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Update(
    [FromServices] IUpdateExpenseUseCase useCase,
    [FromRoute] long id,
    [FromBody] RequestExpenseJson request)
  {
    await useCase.Execute(id, request);

    return NoContent();
  }
}
