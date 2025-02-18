using AutoMapper;
using CashFlow.Application.UseCases.User.Validators;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.User.Create;

public class CreateUserUseCase : ICreateUserUseCase
{
  private readonly IMapper _mapper;
  private readonly IPasswordEncrypter _passwordEncrypter;
  private readonly IUserReadOnlyRepository _userReadOnlyRepository;
  private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IAccessTokenGenerator _tokenGenerator;

  public CreateUserUseCase(
    IMapper mapper, 
    IPasswordEncrypter passwordEncrypter, 
    IUserReadOnlyRepository userReadOnlyRepository, 
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IAccessTokenGenerator tokenGenerator,
    IUnitOfWork unitOfWork)
  {
    _mapper = mapper;
    _passwordEncrypter = passwordEncrypter;
    _userReadOnlyRepository = userReadOnlyRepository;
    _userWriteOnlyRepository = userWriteOnlyRepository;
    _tokenGenerator = tokenGenerator;
    _unitOfWork = unitOfWork;
  }

  public async Task<ResponseCreateUserJson> Execute(RequestUserJson request)
  {
    await Validate(request);

    var user = _mapper.Map<Domain.Entities.User>(request);
    user.Password = _passwordEncrypter.Encrypt(request.Password);
    user.UserIdentifier = Guid.NewGuid();

    await _userWriteOnlyRepository.Add(user);

    await _unitOfWork.Commit();

    return new ResponseCreateUserJson
    {
      Name = user.Name,
      Token = _tokenGenerator.Generate(user)
    };
  }

  private async Task Validate(RequestUserJson request)
  {
    var result = new UserValidator().Validate(request);

    var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

    if (emailExist)
    {
      result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
