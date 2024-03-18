using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TexasHoldem;

public class NameAPIService(HttpClient client)
{
  private readonly HttpClient Client = client;
  private static readonly string nameAPIUrl = "https://api.namefake.com/";

  public async Task<string> GetPlayerName()
  {
    Console.WriteLine("Waiting for a player to join the table...");
    HttpResponseMessage response = await Client.GetAsync(nameAPIUrl);
    response.EnsureSuccessStatusCode();
    string responseData = await response.Content.ReadAsStringAsync();
    string playerName = ParseName(responseData);
    Console.WriteLine($"Player {playerName} sat down.");
    return playerName;
  }

  private static string ParseName(string stringifiesJsonResponse)
  {
    JsonDocument jsonDocument = JsonDocument.Parse(stringifiesJsonResponse);
    jsonDocument.RootElement.TryGetProperty("name", out JsonElement playerNameElement);
    return playerNameElement.ToString();
  }

  public async Task<List<string>> GetPlayerNames()
  {
    List<string> names = [];
    do
    {
      names.Add(await GetPlayerName());
    }while (names.Count < 5);
    return names;
  }


}