namespace TexasHoldem;

public class Player(string name) : IPlayer
{
  public List<Card> cards = new(2);
  public string Name { get; } = name;

  public void ReceiveCard(Card card)
  {
    if (cards.Count == 2)
    {
      throw new InvalidOperationException("The player already has two cards");
    }
    cards.Add(card);
  }

  public void ListCards()
  {
    Console.WriteLine($"\n\nPlayer: {Name}\n");
    foreach (var card in cards)
    {
      Console.WriteLine($"#{cards.IndexOf(card) + 1} ~ {card}");
    }
  }
}