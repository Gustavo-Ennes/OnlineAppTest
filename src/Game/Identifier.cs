namespace TexasHoldem;

public class HandIdentifier:IHandIdentifier
{
  public static bool IsStraight(IEnumerable<IGrouping<int, Card>> scoreGroupedCards)
  {
    List<int> scoreList = scoreGroupedCards
      .Select(cards => cards.ToList()[0].Score)
      .ToList();
    scoreList.Sort();
    // when comparing an element to the previous one
    // the previous has to be 1 unit less than the actual
    // for consecutive times
    int straightMatches = 0;

    if (scoreList.Count < 5) return false;
    foreach (var score in scoreList)
    {
      int scoreIndex = scoreList.IndexOf(score);
      if (scoreIndex == 0) continue;
      if (straightMatches == 4) return true;
      if (
        scoreList[scoreIndex - 1] != score - 1
        // smallest straight with Ace
        || scoreIndex == 1 && score == 2 && scoreList[scoreIndex - 1] != 14
      )
        return false;

      straightMatches += 1;
    }
    return true;
  }

  public static string IdentifyPlayerHand(List<Card> tableCards, List<Card> playerCards)
  {
    List<Card> allCards = [.. playerCards, .. tableCards];
    IEnumerable<IGrouping<string, Card>> groupedByRankCards = allCards.GroupBy(card => card.Rank);
    IEnumerable<IGrouping<string, Card>> groupedBySuitCards = allCards.GroupBy(card => card.Suit);
    IEnumerable<IGrouping<int, Card>> groupedByScoreCards = allCards.GroupBy(card => card.Score);

    // straith-flush
    if (
      IsStraight(groupedByScoreCards)
      && groupedBySuitCards.Any(group => group.Count() == 5)
    )
      return "straightFlush";
    // four of a kind
    if (groupedByRankCards.Any(group => group.Count() == 4))
      return "fourOfAKind";
    // full house
    if (
      groupedByRankCards.Any(group => group.Count() == 3)
      && groupedByRankCards.Any(group => group.Count() == 2)
    )
      return "fullHouse";
    // flush
    if (groupedBySuitCards.Any(group => group.Count() == 5))
      return "flush";
    // straight
    if (IsStraight(groupedByScoreCards))
      return "straight";
    // three of a kind
    if (groupedByRankCards.Any(group => group.Count() == 3))
      return "threeOfAKind";
    // two pair
    if (groupedByRankCards.Count(group => group.Count() == 2) >= 2)
      return "twoPair";
    // pair
    if (groupedByRankCards.Any(group => group.Count() == 2))
      return "pair";
    return "highCard";
  }

}