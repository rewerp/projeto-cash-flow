using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Communication.Emuns;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Expenses.Create;

public class CreateExpenseValidatorTests
{
  [Fact]
  public void Success()
  {
    /* Arrange - Todos os objetos necessários para executar os testes */
    var validator = new CreateExpenseValidator();

    /* Utilizando dados fixos para o teste */
    //var request = new RequestCreateExpenseJson
    //{
    //  Title = "Test Title",
    //  Description = "Test Description",
    //  Amount = 100,
    //  Date = DateTime.Now.AddDays(-1),
    //  PaymentType = CashFlow.Communication.Emuns.PaymentTypeEnum.Cash
    //};

    /* Para não usar dados fixos para os testes, podemos usar o BOGUS, para gerar dados aleatórios */
    var request = RequestCreateExpenseJsonBuilder.Build();

    /* Act - Ações (funções) que serão executadas para validação dos testes */
    var result = validator.Validate(request);

    /* Assert - Validação dos resultados conforme o que se espera como resultado das funções 
     * O Exemplo abaixo é a forma padrão do .NET */

    //Assert.True(result.IsValid);


    /* Podemos utilizar o pacote FluentAssertions, para realizar as validações dos testes
     * de forma mais fluída e com uma sintaxe mais simples 
     * Basta adicionar o using e seguir o exemplo abaixo */

    result.IsValid.Should().BeTrue();
  }

  [Theory]
  [InlineData("")]
  [InlineData("   ")]
  [InlineData(null)]
  public void ErrorTitleEmpty(string title)
  {
    // Arrange
    var validator = new CreateExpenseValidator();
    var request = RequestCreateExpenseJsonBuilder.Build();
    request.Title = title;

    // Act
    var result = validator.Validate(request);

    //Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
  }

  [Fact]
  public void ErrorDateFuture()
  {
    // Arrange
    var validator = new CreateExpenseValidator();
    var request = RequestCreateExpenseJsonBuilder.Build();
    request.Date = DateTime.Now.AddDays(1);

    // Act
    var result = validator.Validate(request);

    //Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSE_CANNOT_FOR_THE_FUTURE));
  }

  [Fact]
  public void ErrorPaymentTypeInvalid()
  {
    // Arrange
    var validator = new CreateExpenseValidator();
    var request = RequestCreateExpenseJsonBuilder.Build();
    request.PaymentType = (PaymentType)700;

    // Act
    var result = validator.Validate(request);

    //Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
  }

  [Theory]
  [InlineData(0)]
  [InlineData(-1)]
  [InlineData(-7)]
  public void ErrorAmountInvalid(decimal amount)
  {
    // Arrange
    var validator = new CreateExpenseValidator();
    var request = RequestCreateExpenseJsonBuilder.Build();
    request.Amount = amount;

    // Act
    var result = validator.Validate(request);

    //Assert
    result.IsValid.Should().BeFalse();
    result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
  }
}
