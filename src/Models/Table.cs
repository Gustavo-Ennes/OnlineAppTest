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
    Console.WriteLine("Table received the flop.");
    Thread.Sleep(500);
  }

  public void ReceiveTurn(Card turnCard)
  {
    if (cards.Count != 3)
    {
      throw new InvalidOperationException("The flop comes before the turn.");
    }
    cards.Add(turnCard);
    Console.WriteLine("Table received the turn.");
    Thread.Sleep(500);
  }

  public void ReceiveRiver(Card riverCard)
  {
    if (cards.Count != 4)
    {
      throw new InvalidOperationException("The flop and turn comes before the river.");
    }
    cards.Add(riverCard);
    Console.WriteLine("Table received the river.");
    Thread.Sleep(500);
  }

  public void ListTableCards()
  {
    string message = $"\n\n---- {Color.ColorizeString("table cards", "cyan")} ----";
    if (cards.Count == 0) message += "No cards in the table.";
    if (cards.Count > 2)
      message += $"\n\n{Color.ColorizeString("Flop", "yellow")}:\n -> {cards[0]}\n -> {cards[1]}\n -> {cards[2]}";
    if (cards.Count > 3)
      message = string.Concat(message, $"\n\n{Color.ColorizeString("Turn", "yellow")}:\n -> {cards[3]}");
    if (cards.Count > 4) message = 
      string.Concat(message, $"\n\n{Color.ColorizeString("River", "yellow")}:\n -> {cards[4]}");

    message += "\n___________________\n\n";

    Console.WriteLine(message);
  }
}