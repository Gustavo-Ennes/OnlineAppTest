namespace TexasHoldemTests;

using TexasHoldem;

public class TableTests
{
  [Fact]
  public void ShouldInstantiateATable()
  {
    Table table = new();
    Assert.NotNull(table);
    Assert.NotNull(table.cards);
    Assert.Equal([], table.cards);
  }
  [Fact]
  public void ShouldReceiveTheFlop()
  {
    Table table = new();
    List<Card> flopCards = [
      new("Ace", "Hearts"),
      new("Queen", "Diamonds"),
      new ("Jack", "Clubs")
    ];
    table.ReceiveFlop(flopCards);

    Assert.Equal(flopCards.Count, table.cards.Count);
    Assert.Contains(flopCards[0], table.cards);
    Assert.Contains(flopCards[1], table.cards);
    Assert.Contains(flopCards[2], table.cards);
  }
  [Fact]
  public void FlopShouldHaveExactly3Cards()
  {
    Table table = new();
    List<Card> lessThanFlopCards = [
      new("Ace", "Hearts"),
    ];
    List<Card> moreThanFlopCards = [
      new("Ace", "Hearts"),
      new("Queen", "Diamonds"),
      new ("Jack", "Clubs"),
      new("10", "Spades")
    ];
    Assert.Throws<InvalidOperationException>(() => table.ReceiveFlop(lessThanFlopCards));
    Assert.Throws<InvalidOperationException>(() => table.ReceiveFlop(moreThanFlopCards));
  }
  [Fact]
  public void ShouldReceiveTheTurn()
  {
    Table table = new();
    List<Card> flopCards = [
      new("Ace", "Hearts"),
      new("Queen", "Diamonds"),
      new ("Jack", "Clubs")
    ];
    Card turnCard = new("10", "Spades");
    table.ReceiveFlop(flopCards);
    table.ReceiveTurn(turnCard);

    Assert.Equal(flopCards.Count + 1 /* turnCard*/, table.cards.Count);
    Assert.Contains(turnCard, table.cards);
  }
  [Fact]
  public void ShouldNotReceiveTheTurnWithoutFlop()
  {
    Table table = new();
    Card turnCard = new("10", "Spades");
    Assert.Throws<InvalidOperationException>(() => table.ReceiveTurn(turnCard));
  }
  [Fact]
  public void ShouldReceiveTheRiver()
  {
    Table table = new();
    List<Card> flopCards = [
      new("Ace", "Hearts"),
      new("Queen", "Diamonds"),
      new ("Jack", "Clubs")
    ];
    Card turnCard = new("10", "Spades");
    Card riverCard = new("9", "Hearts");
    table.ReceiveFlop(flopCards);
    table.ReceiveTurn(turnCard);
    table.ReceiveRiver(riverCard);

    Assert.Equal(flopCards.Count + 2 /* turn, river */, table.cards.Count);
    Assert.Contains(riverCard, table.cards);
  }
}