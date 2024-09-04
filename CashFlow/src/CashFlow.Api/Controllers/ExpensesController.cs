using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
  [HttpPost]
  public IActionResult Create([FromBody] RequestCreateExpenseJson request)
  {
    var response = new CreateExpenseUseCase().Execute(request);
    return Created(string.Empty, response);
  }
}
