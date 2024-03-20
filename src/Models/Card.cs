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

  public static string GetColoredSuit(string suit)
  {
    string spades = "\u2660";
    string diamonds = "\u2666";
    string hearts = "\u2665";
    string clubs = "\u2663";
    return suit switch
    {
      "Spades" => Color.ColorizeString(spades, "black"),
      "Clubs" => Color.ColorizeString(clubs, "black"),
      "Hearts" => Color.ColorizeString(hearts, "red"),
      "Diamonds" => Color.ColorizeString(diamonds, "red"),
      _ => suit
    };
  }

  override public string ToString()
  {
    return $"{(Rank != "10" ? Rank[0] : Rank)}{GetColoredSuit(Suit)} ~ ({Score})";
  }
}


