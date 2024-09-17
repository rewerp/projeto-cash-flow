using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
  [HttpPost]
  public IActionResult Create(
      [FromServices] ICreateExpenseUseCase useCase,
      [FromBody] RequestCreateExpenseJson request)
  {
    //try
    //{
    //  var response = new CreateExpenseUseCase().Execute(request);

    //  return Created(string.Empty, response);
    //}
    //catch (ErrorOnValidationException ex)
    //{
    //  var error = new ResponseErrorJson(ex.Errors);

    //  return BadRequest(error);
    //}
    //catch
    //{
    //  var error = new ResponseErrorJson("Unknow error");

    //  return StatusCode(StatusCodes.Status500InternalServerError, error);
    //}


    // Utilizando o Exception filter, dispensa o uso de Try/Catch
    var response = useCase.Execute(request);

    return Created(string.Empty, response);
  }
}
