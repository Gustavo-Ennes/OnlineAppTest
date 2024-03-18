namespace TexasHoldem;

public class Player(string name) : IPlayer
{
  public List<Card> cards = new(2);
  public Hand? hand;
  public string Name { get; } = name;

  public void ReceiveCard(Card card)
  {
    if (cards.Count == 2)
    {
      throw new InvalidOperationException("The player already has two cards");
    }
    cards.Add(card);
  }

  override public string ToString()
  {
    string str = $"\n\n----- {Name} ------";
    str += "\nCards:\n";
    str += $"\n -> {cards[0]}\n -> {cards[1]}";
    str += $"\n\n{hand}";

    return str;
  }
}