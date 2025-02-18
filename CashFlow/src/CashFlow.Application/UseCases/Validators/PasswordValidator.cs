using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Validators;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
  private const string ERROR_MESSAGE_KEY = "ErrorMessage";

  public override string Name => "PasswordValidator";

  protected override string GetDefaultMessageTemplate(string errorCode)
  {
    //return "{ErrorMessage}";
    return $"{{{ERROR_MESSAGE_KEY}}}";
  }

  public override bool IsValid(ValidationContext<T> context, string password)
  {
    if (string.IsNullOrWhiteSpace(password))
    {
      context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
      return false;
    }

    //if (password.Length < 8)
    //{
    //  context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
    //  return false;
    //}

    //if (!Regex.IsMatch(password, @"[A-Z]+"))
    if (!PasswordValidationRegex().IsMatch(password))
    {
      context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.PASSWORD_INVALID);
      return false;
    }

    return true;
  }

  [GeneratedRegex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")]
  private static partial Regex PasswordValidationRegex();
}
