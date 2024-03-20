namespace TexasHoldem;

public interface IPontuation{
  public static readonly Dictionary<string, int>? HAND_ADDER;
  public static abstract int CalculatePontuation(Hand hand);
  public static abstract int CalculateStraightFlush(Hand hand);
  public static abstract int CalculateFourOfAKind(Hand hand);
  public static abstract int CalculateFullHouse(Hand hand);
  public static abstract int CalculateFlush(Hand hand);
  public static abstract int CalculateStraight(Hand hand);
  public static abstract int CalculateThreeOfAKind(Hand hand);
  public static abstract int CalculateTwoPair(Hand hand);
  public static abstract int CalculatePair(Hand hand);
  public static abstract int CalculateHighCard(Hand hand);
}