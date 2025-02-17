using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class NotFoundException : CashFlowException
{
  public override int StatusCode => (int)HttpStatusCode.NotFound;

  public NotFoundException(string message) : base(message)
  {
  }

  public override List<string> GetErrors()
  {
    // Forma regular
    //return new List<string>() { Message };

    // Forma reduzida
    return [Message];
  }
}
