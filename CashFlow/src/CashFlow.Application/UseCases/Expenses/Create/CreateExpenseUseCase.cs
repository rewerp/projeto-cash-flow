using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Create;

public class CreateExpenseUseCase : ICreateExpenseUseCase
{
  // o atributo ReadOnly, vai permitir que essa propriedade seja alterada apenas dentro do construtor
  private readonly IExpensesRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public ResponseCreateExpenseJson Execute(RequestCreateExpenseJson request)
  {
    Validate(request);

    var entity = new Expense
    {
      Title = request.Title,
      Description = request.Description,
      Date = request.Date,
      Amount = request.Amount,
      PaymentType = (Domain.Enums.PaymentType)request.PaymentType
    };

    _repository.Add(entity);

    _unitOfWork.Commit();

    return new ResponseCreateExpenseJson();
  }

  private void Validate(RequestCreateExpenseJson request)
  {
    var result = new CreateExpenseValidator().Validate(request);

    if (!result.IsValid)
    {
      var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

      throw new ErrorOnValidationException(errorMessages);
    }
  }
}
