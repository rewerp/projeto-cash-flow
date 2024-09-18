using CashFlow.Application.UseCases.Expenses.Create;
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
      [FromBody] RequestCreateExpenseJson request)
  {
    // Utilizando o Exception filter, dispensa o uso de Try/Catch
    var response = await useCase.Execute(request);

    return Created(string.Empty, response);
  }
}
