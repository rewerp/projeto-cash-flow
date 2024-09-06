using CashFlow.Communication.Emuns;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses;

public class CreateExpenseUseCase
{
  public ResponseCreateExpenseJson Execute(RequestCreateExpenseJson request)
  {
    Validate(request);

    return new ResponseCreateExpenseJson();
  }

  private void Validate(RequestCreateExpenseJson request)
  {
    var result = new CreateExpenseValidator().Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

      throw new ArgumentException();
    }
  }
}
