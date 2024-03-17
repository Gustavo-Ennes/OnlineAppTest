namespace TexasHoldemTests;

using TexasHoldem;

public class PlayerTests
{
  private readonly string BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER = "Guthrie Govan";

  [Fact]
  public void ShouldInstantiateAPlayer()
  {
    Player player = new(BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER);
    Assert.NotNull(player);
    Assert.NotNull(player.Name);
    Assert.Equal(BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER, player.Name);
    Assert.NotNull(player.cards);
    Assert.Equal([], player.cards);
  }
  [Fact]
  public void ShouldReceiveACard()
  {
    Player player = new(BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER);
    Card card = new("Ace", "Hearts");
    player.ReceiveCard(card);

    Assert.Contains(card, player.cards);
    Assert.Equal(card, player.cards[0]);
  }
  [Fact]
  public void ShouldPlayerHasOnlyTwoCards()
  {
    Player player = new(BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER);
    Card card1 = new("Ace", "Hearts");
    Card card2 = new("Ace", "Spades");
    Card card3 = new("Ace", "Diamonds");
    player.ReceiveCard(card1);
    player.ReceiveCard(card2);
    Assert.Throws<InvalidOperationException>(() => player.ReceiveCard(card3));
  }
}