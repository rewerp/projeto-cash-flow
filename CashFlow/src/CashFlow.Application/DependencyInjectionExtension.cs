using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.User.Create;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
  public static void AddApplication(this IServiceCollection services)
  {
    AddAutoMapper(services);
    AddUsesCases(services);
  }

  private static void AddAutoMapper(IServiceCollection services)
  {
    services.AddAutoMapper(typeof(AutoMapping));
  }

  private static void AddUsesCases(IServiceCollection services)
  {
    services.AddScoped<ICreateExpenseUseCase, CreateExpenseUseCase>();
    services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
    services.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
    services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
    services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
    services.AddScoped<IGenerateExpensesReportExcelUseCase, GenerateExpensesReportExcelUseCase>();
    services.AddScoped<IGenerateExpensesReportPdfUseCase, GenerateExpensesReportPdfUseCase>();
    services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
  }
}
