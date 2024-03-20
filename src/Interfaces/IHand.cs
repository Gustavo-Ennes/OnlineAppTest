namespace TexasHoldem;

public interface IHand
{
  public void BuildHand();
  public void BuildHighCardHand();
  public void BuildPairHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards);
  public void BuildTwoPairHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards);
  public void BuildThreeOfAKindHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards);
  public void BuildStraightHand();
  public void BuildFlushHand(IEnumerable<IGrouping<string, Card>> groupedBySuitCards);
  public void BuildFullHouseHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards);
  public void BuildFourOfAKindHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards);
  public void BuildStraightFlushHand();
  public void DefineScore();
}