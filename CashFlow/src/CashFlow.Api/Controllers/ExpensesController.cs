using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
  [HttpPost]
  public IActionResult Create([FromBody] RequestCreateExpenseJson request)
  {
    try
    {
      var response = new CreateExpenseUseCase().Execute(request);

      return Created(string.Empty, response);
    }
    catch (ArgumentException ex)
    {
      var error = new ResponseErrorJson(errorMessage: ex.Message);

      return BadRequest(error);
    }
    catch
    {
      var error = new ResponseErrorJson(errorMessage: "Unknow error");

      return StatusCode(StatusCodes.Status500InternalServerError, error);
    }
  }
}
