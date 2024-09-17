using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Create;

public interface ICreateExpenseUseCase
{
  ResponseCreateExpenseJson Execute(RequestCreateExpenseJson request);
}
