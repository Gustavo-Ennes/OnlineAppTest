namespace TexasHoldemTests;

using TexasHoldem;

public class HandTests
{
  readonly List<Card> playerCards = [
      new("Ace", "Clubs"),
      new("9", "Diamonds")
  ];

  readonly List<Card> tableCards = [
      new("King", "Spades"),
      new("Ace", "Clubs"),
      new("5", "Hearts"),
      new("3", "Spades"),
      new("Jack", "Diamonds")
  ];

  [Fact]
  public void ShouldBuildAStraightFlushHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    List<int> straightCardScores = [2, 3, 4, 5, 14];
    Hand hand;
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("2", "Clubs"));
    modifiedTableCards.RemoveAt(1);
    modifiedTableCards.Insert(1, new Card("3", "Clubs"));
    modifiedTableCards.RemoveAt(2);
    modifiedTableCards.Insert(2, new Card("4", "Clubs"));
    modifiedTableCards.RemoveAt(3);
    modifiedTableCards.Insert(3, new Card("5", "Clubs"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("straightFlush", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    foreach (var card in hand.Cards)
    {
      Assert.Equal("Clubs", card.Suit);
      Assert.True(hand.Cards.All(card => straightCardScores.Contains(card.Score)));
      Assert.Contains(hand.Cards, card => card.Rank == "Ace");
    }
  }
  [Fact]
  public void ShouldBuildAFourOfAKindHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand;
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("Ace", "Diamonds"));
    modifiedTableCards.RemoveAt(2);
    modifiedTableCards.Insert(2, new Card("Ace", "Spades"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("fourOfAKind", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Contains(hand.Cards.GroupBy(card => card.Rank), group => group.Count() == 4);
  }
  [Fact]
  public void ShouldBuildAFullHouseHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand;
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("9", "Diamonds"));
    modifiedTableCards.RemoveAt(2);
    modifiedTableCards.Insert(2, new Card("Ace", "Diamonds"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("fullHouse", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Contains(hand.Cards.GroupBy(card => card.Rank), group => group.Count() == 3);
    Assert.Contains(hand.Cards.GroupBy(card => card.Rank), group => group.Count() == 2);
  }
  [Fact]
  public void ShouldBuildAFlushHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand;
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("2", "Diamonds"));
    modifiedTableCards.RemoveAt(1);
    modifiedTableCards.Insert(1, new Card("4", "Diamonds"));
    modifiedTableCards.RemoveAt(2);
    modifiedTableCards.Insert(2, new Card("Queen", "Diamonds"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("flush", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Contains(hand.Cards.GroupBy(card => card.Suit), group => group.Count() == 5);
  }
  [Fact]
  public void ShouldBuildAStraightHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand;
    List<int> straightCardScores = [2, 3, 4, 5, 14];
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("2", "Clubs"));
    modifiedTableCards.RemoveAt(1);
    modifiedTableCards.Insert(1, new Card("4", "Spades"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("straight", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.True(hand.Cards.All(card => straightCardScores.Contains(card.Score)));
  }
  [Fact]
  public void ShouldBuildAThreeOfAKindHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand;
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("9", "Clubs"));
    modifiedTableCards.RemoveAt(1);
    modifiedTableCards.Insert(1, new Card("9", "Spades"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("threeOfAKind", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Contains(hand.Cards.GroupBy(card => card.Rank), group => group.Count() == 3);
  }
  [Fact]
  public void ShouldBuildATwoPairHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand;
    modifiedTableCards.RemoveAt(0);
    modifiedTableCards.Insert(0, new Card("9", "Clubs"));
    hand = new(modifiedTableCards, playerCards);

    Assert.Equal("twoPair", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Equal(2, hand.Cards.GroupBy(card => card.Rank).Count(group => group.Count() == 2));
  }
  [Fact]
  public void ShouldBuildAPairHand()
  {
    List<Card> modifiedTableCards = [.. tableCards];
    Hand hand = new(modifiedTableCards, playerCards);
    Assert.Equal("pair", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Contains(hand.Cards.GroupBy(card => card.Rank), group => group.Count() == 2);
  }
  [Fact]
  public void ShouldBuildAHighCardHand()
  {
    List<Card> modifiedPlayerCards = [.. playerCards];
    Hand hand;
    modifiedPlayerCards.RemoveAt(0);
    modifiedPlayerCards.Insert(0, new Card("6", "Diamonds"));
    hand = new(tableCards, modifiedPlayerCards);

    Assert.Equal("highCard", hand.type);
    Assert.NotNull(hand.Score);
    Assert.Equal(5, hand.Cards.Count);
    Assert.Equal(5, hand.Cards.GroupBy(card => card.Rank).Count(group => group.Count() == 1));
  }
}
