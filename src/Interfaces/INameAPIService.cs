namespace TexasHoldem;

public interface INameAPIService
{
  public Task<string> GetPlayerName();
  string ParseName(string stringifiesJsonResponse);
  public List<string> GetPlayerNames();
}