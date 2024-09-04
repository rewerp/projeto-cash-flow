using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses;

public class CreateExpenseUseCase
{
  public ResponseCreateExpenseJson Execute(RequestCreateExpenseJson request)
  {
    return new ResponseCreateExpenseJson();
  }
}
