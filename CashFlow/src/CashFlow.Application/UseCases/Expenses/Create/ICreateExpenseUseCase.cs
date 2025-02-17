using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Create;

public interface ICreateExpenseUseCase
{
  Task<ResponseCreateExpenseJson> Execute(RequestExpenseJson request);
}
