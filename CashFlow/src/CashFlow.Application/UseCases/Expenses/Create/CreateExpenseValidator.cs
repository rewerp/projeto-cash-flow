using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Create;

/* Classe com a finalidade de realizar validações para a classe CreateExpenseUseCase
 * Dessa forma, ao validar os elementos de uma Request, dispensa o uso de uma cadeia de IFs
 */

public class CreateExpenseValidator : AbstractValidator<RequestCreateExpenseJson>
{
  public CreateExpenseValidator()
  {
    RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
    RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.EXPENSE_CANNOT_FOR_THE_FUTURE);
    RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
  }
}
