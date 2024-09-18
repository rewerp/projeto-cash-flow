using AutoMapper;
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
  private readonly IMapper _mapper;

  public CreateExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<ResponseCreateExpenseJson> Execute(RequestCreateExpenseJson request)
  {
    Validate(request);

    //var entity = new Expense
    //{
    //  Title = request.Title,
    //  Description = request.Description,
    //  Date = request.Date,
    //  Amount = request.Amount,
    //  PaymentType = (Domain.Enums.PaymentType)request.PaymentType
    //};

    var entity = _mapper.Map<Expense>(request);

    await _repository.Add(entity);

    await _unitOfWork.Commit();

    return _mapper.Map<ResponseCreateExpenseJson>(entity);
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
