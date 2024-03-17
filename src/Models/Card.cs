namespace TexasHoldem;

public class Card : ICard
{
  public string Suit { get; }
  public string Rank { get; }
  public int Score { get; }
  public static readonly List<string> AllRanks = [
    "2",
    "3",
    "4",
    "5",
    "6",
    "7",
    "8",
    "9",
    "10",
    "Jack",
    "Queen",
    "King",
    "Ace"
  ];
  public static readonly List<string> AllSuits = ["Hearts", "Diamonds", "Clubs", "Spades"];

  public Card(string constructorRank, string constructorSuit)
  {
    if (
      !AllSuits.Any(suit => suit.Equals(constructorSuit)) ||
      !AllRanks.Any(rank => rank.Equals(constructorRank))
    )
      throw new InvalidOperationException(
        $"{constructorRank} of {constructorSuit} isn't a deck card."
    );
    this.Suit = constructorSuit;
    this.Rank = constructorRank;
    this.Score = CalculateScore();
  }

  public int CalculateScore()
  {
    if (char.IsDigit(Rank[0]))
    {
      return int.Parse(Rank);
    }
    // dahora
    return Rank switch
    {
      "Jack" => 11,
      "Queen" => 12,
      "King" => 13,
      "Ace" => 14,
      _ => throw new InvalidOperationException($"{Rank} isn't a deck card."),
    };
  }

  override public string ToString()
  {
    return $"{Rank} of {Suit} ({Score})";
  }
}


