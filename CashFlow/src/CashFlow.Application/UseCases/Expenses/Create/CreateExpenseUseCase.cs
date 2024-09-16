using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Create;

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

      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
