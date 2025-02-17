using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services, configuration);
    AddRepositories(services);
    AddSecurities(services);
  }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("Connection");

    var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));

    services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
  }

  private static void AddRepositories(IServiceCollection services)
  {
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
    services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
    services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
    services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
  }

  private static void AddSecurities(IServiceCollection services)
  {
    services.AddScoped<IPasswordEncrypter, Secutiry.BCrypt>();
  }
}
