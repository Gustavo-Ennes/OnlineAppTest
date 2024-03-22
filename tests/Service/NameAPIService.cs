namespace TexasHoldemTests;

using System.Net;
using Moq;
using TexasHoldem;

public class NameAPIServiceTests
{
  public static readonly string nameAPIUrl = "https://api.namefake.com/";
  // Arrange
  public readonly Mock mockAPIService = new Mock<INameAPIService>();

  [Fact]
  public async void ShouldGetThePlayerName()
  {
    var mockHttpClient = new Mock<IHttpClientWrapper>();
    mockHttpClient
      .Setup(x => x.GetAsync(It.IsAny<string>()))
      .ReturnsAsync(
        new HttpResponseMessage(HttpStatusCode.OK)
        {
          Content = new StringContent("{\"name\": \"John\"}")
        });

    var nameServiceApi = new NameAPIService(mockHttpClient.Object);

    // Act
    string name = await nameServiceApi.GetPlayerName();
    List<string> headsUpNames = await nameServiceApi.GetPlayerNames(1); //modality 1: Heads-up
    List<string> sixMaxNames = await nameServiceApi.GetPlayerNames(2); //modality 2: 6-max
    List<string> fullRingNames = await nameServiceApi.GetPlayerNames(3); //modality 3: full-ring

    // Assert
    Assert.Equal("John", name);
    Assert.Single(headsUpNames); // 2 players - 1 realPlayer
    Assert.Equal(5, sixMaxNames.Count); // 6 players - 1 realPlayer, etc..
    Assert.Equal(8, fullRingNames.Count);
  }
}
