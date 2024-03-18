namespace TexasHoldemTests;

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
    // Arrange
    var mockAPIService = new Mock<INameAPIService>();
    mockAPIService
      .Setup(service => service.GetPlayerName())
      .ReturnsAsync("MockPlayer");

    string playerName = await mockAPIService.Object.GetPlayerName();
    Assert.Equal("MockPlayer", playerName);
  }
}
