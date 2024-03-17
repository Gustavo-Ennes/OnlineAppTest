namespace TexasHoldem;

public class Deck : IDeck
{
  public readonly List<Card> cards = [];

  public Deck()
  {
    Initialize();
  }

  public void Initialize()
  {
    foreach (var suit in Card.AllSuits)
    {
      foreach (var rank in Card.AllRanks)
      {
        cards.Add(new Card(rank, suit));
      }
    }
  }

  public void Shuffle()
  {
    Random rng = new();
    int n = cards.Count;
    while (n > 1)
    {
      n--;
      int k = rng.Next(n + 1);
      (cards[n], cards[k]) = (cards[k], cards[n]);
    }
  }

  public List<Card> DealCards(int quantity = 1)
  {
    List<Card> cardsToDeal = [];
    if (cards.Count < quantity)
    {
      throw new InvalidOperationException($"The deck hasn't {quantity} cards to deal, it has only {cards.Count}");
    }
    foreach (var _ in Enumerable.Range(0, quantity))
    {
      Card card = cards[0];
      cards.RemoveAt(0);
      cardsToDeal.Add(card);
    }
    return cardsToDeal;
  }

  public void ListCards()
  {
    foreach (var card in cards)
    {
      Console.WriteLine($"#{cards.IndexOf(card) + 1} ~ {card}");
    }
  }
}

