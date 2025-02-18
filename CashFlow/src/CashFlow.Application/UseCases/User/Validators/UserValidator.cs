using CashFlow.Application.UseCases.Validators;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.User.Validators;

public class UserValidator : AbstractValidator<RequestUserJson>
{
  public UserValidator()
  {
    RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
    RuleFor(user => user.Email)
      .NotEmpty()
      .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
      .EmailAddress()
      .WithMessage(ResourceErrorMessages.EMAIL_INVALID);

    RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUserJson>());
  }
}
