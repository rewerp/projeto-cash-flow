using CashFlow.Application.UseCases.User.Create;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
  [HttpPost]
  [ProducesResponseType(typeof(ResponseCreateUserJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Create(
    [FromServices] ICreateUserUseCase useCase,
    [FromBody] RequestUserJson request)
  {
    var response = await useCase.Execute(request);

    return Created(string.Empty, response);
  }
}
