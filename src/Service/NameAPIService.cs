using System.Text.Json;

namespace TexasHoldem;

public class NameAPIService(IHttpClientWrapper client)
{
  private readonly IHttpClientWrapper Client = client;
  private static readonly string nameAPIUrl = "https://api.namefake.com/";

  public async Task<string> GetPlayerName()
  {
    Console.WriteLine("\nWaiting for a player to join the table...");
    HttpResponseMessage response = await Client.GetAsync(nameAPIUrl);
    response.EnsureSuccessStatusCode();
    string responseData = await response.Content.ReadAsStringAsync();
    string playerName = ParseName(responseData);
    Console.WriteLine($"Player {Color.ColorizeString(playerName, "cyan")} sat down.");
    return playerName;
  }

  private static string ParseName(string stringifiesJsonResponse)
  {
    JsonDocument jsonDocument = JsonDocument.Parse(stringifiesJsonResponse);
    jsonDocument.RootElement.TryGetProperty("name", out JsonElement playerNameElement);
    return playerNameElement.ToString();
  }

  public async Task<List<string>> GetPlayerNames(int modalityNumber)
  {
    int numberOfPlayers = modalityNumber switch
    {
      1 => 1,
      2 => 5,
      3 => 8,
      _ => 5
    };
    List<string> names = [];

    do
    {
      names.Add(await GetPlayerName());
    } while (names.Count < numberOfPlayers);
    return names;
  }


}