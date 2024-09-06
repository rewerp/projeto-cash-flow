using CashFlow.Communication.Emuns;
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses;

public class CreateExpenseValidator : AbstractValidator<RequestCreateExpenseJson>
{
  public CreateExpenseValidator()
  {
    RuleFor(expense => expense.Title).NotEmpty().WithMessage("The Title is required.");
    RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The Amount must be greater than zero.");
    RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expenses cannot be for the future.");
    RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment Type is not valid.");
  }
}
