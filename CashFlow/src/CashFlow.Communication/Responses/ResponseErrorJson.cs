namespace CashFlow.Communication.Responses;

public class ResponseErrorJson
{
  public List<string> ErrorMessages { get; set; }

  public ResponseErrorJson(string errorMessage)
  {
    // Sintaxe tradicional
    //ErrorMessages = new List<string> { errorMessage };

    // Sintaxe simplificado para criar um List<> inicializando com um valor
    ErrorMessages = [errorMessage];
  }

  public ResponseErrorJson(List<string> errorMessages)
  {
    ErrorMessages = errorMessages;
  }
}
