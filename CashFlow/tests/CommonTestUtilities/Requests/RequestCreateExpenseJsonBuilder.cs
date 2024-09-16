using Bogus;
using CashFlow.Communication.Emuns;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestCreateExpenseJsonBuilder
{
  public static RequestCreateExpenseJson Build()
  {
    /* Primeira forma de usar o Faker do BOGUS */

    //var faker = new Faker();

    //return new RequestCreateExpenseJson
    //{
    //  Title = faker.Finance.AccountName(),
    //  Description = faker.Finance.AccountName(),
    //  Amount = faker.Finance.Amount(),
    //  Date = faker.Date.Past(),
    //  PaymentType = faker.PickRandom<PaymentTypeEnum>()
    //};


    /* Forma alternativa de usar o Faker do BOGUS */

    return new Faker<RequestCreateExpenseJson>()
      .RuleFor(r => r.Title, faker => faker.Finance.AccountName())
      .RuleFor(r => r.Description, faker => faker.Finance.AccountName())
      .RuleFor(r => r.Amount, faker => faker.Finance.Amount())
      .RuleFor(r => r.Date, faker => faker.Date.Past())
      .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>());
  }
}
