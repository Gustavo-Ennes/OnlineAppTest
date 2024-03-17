namespace TexasHoldem;

public class Pontuation: IPontuation
{
  public static readonly Dictionary<string, int> HAND_ADDER = new()
  {
    {"highCard", 1},
    {"pair", 100},
    {"twoPair", 2*100},
    {"threeOfAKind", 3*100},
    {"straight", 4*100},  
    {"flush", 5*100},
    {"fullHouse",6*100},
    {"fourOfAKind", 7*100},
    {"straightFlush", 8*100},
  };
}