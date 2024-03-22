namespace TexasHoldem;

public class HttpClientWrapper(HttpClient httpClient) : IHttpClientWrapper
{
  private readonly HttpClient _httpClient = httpClient;

  public async Task<HttpResponseMessage> GetAsync(string requestUri)
  {
    return await _httpClient.GetAsync(requestUri);
  }
}
