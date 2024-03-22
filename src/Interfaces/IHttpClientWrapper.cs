namespace TexasHoldem;

public interface IHttpClientWrapper
{
  Task<HttpResponseMessage> GetAsync(string requestUri);
}
