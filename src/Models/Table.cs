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
    Thread.Sleep(200);
  }

  public void ReceiveTurn(Card turnCard)
  {
    if (cards.Count != 3)
    {
      throw new InvalidOperationException("The flop comes before the turn.");
    }
    cards.Add(turnCard);
    Console.WriteLine("Table received the turn.");
    Thread.Sleep(200);
  }

  public void ReceiveRiver(Card riverCard)
  {
    if (cards.Count != 4)
    {
      throw new InvalidOperationException("The flop and turn comes before the river.");
    }
    cards.Add(riverCard);
    Console.WriteLine("Table received the river.");
    Thread.Sleep(200);
  }

  public override string ToString()
  {
    string str = "[ ";
    foreach(Card card in cards.GetRange(0,3))
    {
      str += $" {card}";
    }
    str += $" ]  [ {cards[3]} ]  [ {cards[4]} ]\n";
    return str;
  }
}