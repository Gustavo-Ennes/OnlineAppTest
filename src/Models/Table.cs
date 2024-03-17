namespace TexasHoldem;

public class Table
{
  public List<Card> cards = [];

  public void ReceiveFlop(List<Card> flopCards)
  {
    if (flopCards.Count != 3)
    {
      throw new InvalidOperationException("The flop must contains three cards.");
    }
    foreach (Card flopCard in flopCards)
    {
      cards.Add(flopCard);
    }
  }

  public void ReceiveTurn(Card turnCard)
  {
    if (cards.Count != 3)
    {
      throw new InvalidOperationException("The flop comes before the turn.");
    }
    cards.Add(turnCard);
  }

  public void ReceiveRiver(Card riverCard)
  {
    if (cards.Count != 4)
    {
      throw new InvalidOperationException("The flop and turn comes before the river.");
    }
    cards.Add(riverCard);
  }

  public void ListTableCards()
  {
    string message = "";
    if (cards.Count == 0) message = "No cards in the table.";
    if (cards.Count > 2)
      message = $"\n\nFlop:\n -> {cards[0]}\n -> {cards[1]}\n -> {cards[2]}";
    if (cards.Count > 3)
      message = string.Concat(message, $"\n\nTurn:\n -> {cards[3]}");
    if (cards.Count > 4) message = string.Concat(message, $"\n\nRiver:\n -> {cards[4]}");

    Console.WriteLine(message);
  }
}