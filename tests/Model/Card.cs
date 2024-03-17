namespace TexasHoldemTests;

using TexasHoldem;

public class CardTests
{
  [Fact]
  public void ShouldInstantiateACardCorrectly()
  {
    string expectedRank = "Ace";
    string expectedSuit = "Diamonds";

    Card card = new(expectedRank, expectedSuit);
    Assert.NotNull(card);
    Assert.IsType<Card>(card);
    Assert.Equal(expectedRank, card.Rank);
    Assert.Equal(expectedSuit, card.Suit);
  }
  [Fact]
  public void ShouldThrowAnErrorIfNotADeckCard()
  {
    string invalidRank = "Aces";
    string validSuit = "High";
    Assert.Throws<InvalidOperationException>(() => new Card(invalidRank, validSuit));
  }
  [Fact]
  public void ShouldCalculateScoreCorrectly()
  {
    Card card1 = new("Ace", "Diamonds");
    Card card2 = new("3", "Spades");
    Card card3 = new("Queen", "Clubs");

    Assert.Equal(14, card1.Score);
    Assert.Equal(3, card2.Score);
    Assert.Equal(12, card3.Score);
  }
}