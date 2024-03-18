namespace TexasHoldem;

//factory pattern
public interface ICard{
  public string Suit { get; }
  public string Rank { get; }
  public int Score { get; }
  public static readonly List<string>? AllRanks;
  public static readonly List<string>? AllSuits;
  public int CalculateScore();
  abstract static string GetColoredSuit(string suit);
}