namespace TexasHoldemTests;

using TexasHoldem;

public class DealerTests
{
  public readonly string BADASS_DRUMMER_NAME_FOR_FAKE_PLAYER = "Mario Duplantier";
  [Fact]
  public void ShouldInstantiateTheDealer()
  {
    Dealer dealer = new(new Deck(), new Table());
    Assert.NotNull(dealer);
    Assert.NotNull(dealer.Deck);
    Assert.NotNull(dealer.Table);
  }
  [Fact]
  public void ShouldDistributeACardToAPlayer()
  {
    Dealer dealer = new(new Deck(), new Table());
    Player player = new(BADASS_DRUMMER_NAME_FOR_FAKE_PLAYER);
    dealer.DistributeACardTo(player);

    Assert.Equal(51, dealer.Deck.cards.Count);
    Assert.Single(player.cards);
    Assert.DoesNotContain(player.cards[0], dealer.Deck.cards);
  }
  [Fact]
  public void ShouldGetCardsFromTheDeck()
  {
    Dealer dealer = new(new Deck(), new Table());
    List<Card> cards = dealer.GetCardsFromDeck(3);

    Assert.Equal(3, cards.Count);
    Assert.Equal(49, dealer.Deck.cards.Count);
    Assert.DoesNotContain(cards[0], dealer.Deck.cards);
    Assert.DoesNotContain(cards[1], dealer.Deck.cards);
    Assert.DoesNotContain(cards[2], dealer.Deck.cards);
  }
  [Fact]
  public void ShouldBurnACard()
  {
    Dealer dealer = new(new Deck(), new Table());
    dealer.BurnACard();
    Assert.Equal(51, dealer.Deck.cards.Count);
    dealer.BurnACard();
    Assert.Equal(50, dealer.Deck.cards.Count);
  }
}