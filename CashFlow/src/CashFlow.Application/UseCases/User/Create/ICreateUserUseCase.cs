using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.User.Create;

public interface ICreateUserUseCase
{
  Task<ResponseCreateUserJson> Execute(RequestUserJson request);
}
