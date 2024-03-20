namespace TexasHoldem;

public class HandIdentifier : IHandIdentifier
{
  public static bool IsLowestStraight(Hand hand)
  {
    hand.Cards.Sort((c1, c2) => c2.Score.CompareTo(c1.Score));
    return (hand.type == "straight" || hand.type == "straightFlush")
      && hand.Cards[1].Rank == "5"
      && hand.Cards[4].Rank == "2";
  }
  public static bool IsStraight(IEnumerable<IGrouping<int, Card>> scoreGroupedCards)
  {
    List<int> scoreList = scoreGroupedCards
      .Select(cards => cards.ToList()[0].Score)
      .ToList();
    // Convert 14 to 1 if present in the list, Ace low straight
    for (int i = 0; i < scoreList.Count; i++)
    {
      if (scoreList[i] == 14)
        scoreList.Add(1);
    }
    scoreList.Sort();
    // Check for 5 consecutive values
    for (int i = 0; i <= scoreList.Count - 5; i++)
    {
      if (scoreList[i] == scoreList[i + 1] - 1 &&
          scoreList[i + 1] == scoreList[i + 2] - 1 &&
          scoreList[i + 2] == scoreList[i + 3] - 1 &&
          scoreList[i + 3] == scoreList[i + 4] - 1)
      {
        return true;
      }
    }
    return false;
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