using CashFlow.Application.UseCases.Login.Validators;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
  private readonly IUserReadOnlyRepository _repository;
  private readonly IPasswordEncrypter _passwordEncrypter;
  private readonly IAccessTokenGenerator _accessTokenGenerator;

  public DoLoginUseCase(
    IUserReadOnlyRepository repository,
    IPasswordEncrypter passwordEncrypter,
    IAccessTokenGenerator accessTokenGenerator)
  {
    _repository = repository;
    _passwordEncrypter = passwordEncrypter;
    _accessTokenGenerator = accessTokenGenerator;
  }

  public async Task<ResponseCreateUserJson> Execute(RequestLoginJson request)
  {
    Validate(request);

    var user = await _repository.GetUserByEmail(request.Email);

    if (user is null)
    {
      throw new InvalidLoginException();
    }

    var passwordMatch = _passwordEncrypter.Verify(request.Password, user.Password);

    if (!passwordMatch)
    {
      throw new InvalidLoginException();
    }

    return new ResponseCreateUserJson
    {
      Name = user.Name,
      Token = _accessTokenGenerator.Generate(user)
    };
  }

  private void Validate(RequestLoginJson request)
  {
    var result = new LoginValidator().Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
