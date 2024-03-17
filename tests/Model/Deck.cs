namespace TexasHoldemTests;

using TexasHoldem;

public class DeckTests
{
  public readonly Deck inalteredDeck = new();

  [Fact]
  public void ShouldInstantiateAndInitializeTheDeck()
  {
    Deck deck = new();
    Assert.NotNull(deck);
    Assert.NotNull(deck.cards);
    Assert.Equal(52, deck.cards.Count);
  }
  [Fact]
  public void ShouldDealCards()
  {
    Deck deck = new();
    List<Card> dealedCards = [deck.cards[0], deck.cards[1]];
    deck.DealCards(2);

    Assert.Equal(50, deck.cards.Count);
    Assert.DoesNotContain(dealedCards[0], deck.cards);
    Assert.DoesNotContain(dealedCards[1], deck.cards);
  }
  [Fact]
  public void ShouldShuffleTheDeck()
  {
    Deck shuffledDeck = new();
    shuffledDeck.Shuffle();

    foreach (var shuffledCard in shuffledDeck.cards)
    {
      int deckIndex = shuffledDeck.cards.IndexOf(shuffledCard);
      Assert.NotEqual(shuffledCard, inalteredDeck.cards[deckIndex]);
    }
  }
}