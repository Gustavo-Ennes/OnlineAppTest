namespace TexasHoldem;

public class Hand : IHand
{
  public List<Card> AllCards, Cards = [];
  public string? type;
  public int? Score;

  public Hand(List<Card> tableCards, List<Card> playerCards)
  {
    AllCards = [.. playerCards, .. tableCards];

    if (AllCards.Count != 7)
      throw new InvalidOperationException(
        "Two player cards and 5 table cards is needed to initialize a hand."
    );

    type = HandIdentifier.IdentifyPlayerHand(tableCards, playerCards);
    BuildHand();
    DefineScore();
  }

  public void BuildHand()
  {
    IEnumerable<IGrouping<string, Card>> groupedByRankCards = AllCards.GroupBy(card => card.Rank);
    IEnumerable<IGrouping<string, Card>> groupedBySuitCards = AllCards.GroupBy(card => card.Suit);
    IEnumerable<IGrouping<int, Card>> groupedByScoreCards = AllCards.GroupBy(card => card.Score);

    //straightFlush
    if (
      HandIdentifier.IsStraight(groupedByScoreCards)
      && groupedBySuitCards.Any(group => group.Count() == 5)
    )
    {
      Cards = [.. groupedBySuitCards.Where(group => group.Count() == 5).ToList()[0]];
    }
    //fourOfAKind
    else if (groupedByRankCards.Any(group => group.Count() == 4))
    {
      List<Card> fourOfAKindCards =
        [.. groupedByRankCards.Where(group => group.Count() == 4).ToList()[0]];
      List<Card> otherCards =
        AllCards.Where(card => card.Rank != fourOfAKindCards[0].Rank).ToList();
      Card? highestCardOfOtherCards =
        otherCards.OrderByDescending(card => card.Score).FirstOrDefault();

      Cards = fourOfAKindCards;
      if (highestCardOfOtherCards != null)
      {
        Cards.Add(highestCardOfOtherCards);
      }
    }
    //fullHouse
    else if (
      groupedByRankCards.Any(group => group.Count() == 3)
      && groupedByRankCards.Any(group => group.Count() == 2)
    )
    {
      List<Card> threeEqualCards =
        [.. groupedByRankCards.Where(group => group.Count() == 3).ToList()[0]];
      List<Card> pairCards =
        [.. groupedByRankCards.Where(group => group.Count() == 2).ToList()[0]];
      Cards = [.. threeEqualCards, .. pairCards];
    }
    //flush
    else if (
      groupedBySuitCards.Any(group => group.Count() == 5)
    )
    {
      Cards =
       [.. groupedBySuitCards.Where(group => group.Count() == 5).ToList()[0]];
    }
    // straight
    else if (
      HandIdentifier.IsStraight(groupedByScoreCards)
    )
    {
      AllCards.Sort((card1, card2) => card1.Score.CompareTo(card2.Score));
      for (int i = 0; i <= AllCards.Count - 5; i++)
      {
        if (
          AllCards[i].Score == AllCards[i + 1].Score - 1 &&
          AllCards[i].Score == AllCards[i + 2].Score - 2 &&
          AllCards[i].Score == AllCards[i + 3].Score - 3 &&
          (AllCards[i].Score == AllCards[i + 4].Score - 4
            || AllCards[i].Score == AllCards[i + 4].Score + 10)
        )
        {
          Cards = AllCards.GetRange(i, 5);
        }
      }
    }
    //threeOfAKind
    else if (
      groupedByRankCards.Any(group => group.Count() == 3)
    )
    {
      List<Card> threeOfAKind =
        [.. groupedByRankCards.Where(group => group.Count() == 3).ToList()[0]];
      List<Card> otherCards =
        AllCards.Where(card => card.Rank != threeOfAKind[0].Rank).ToList();
      otherCards.Sort((card1, card2) => card1.Score.CompareTo(card2.Score));

      Cards = threeOfAKind;
      Cards.Add(otherCards[^1]);
      Cards.Add(otherCards[^2]);
    }
    //two pair
    else if (
      groupedByRankCards.Count(group => group.Count() == 2) >= 2
    )
    {
      List<Card> twoPairCards =
        [
          .. groupedByRankCards.Where(group => group.Count() == 2).ToList()[0],
          .. groupedByRankCards.Where(group => group.Count() == 2).ToList()[1],
        ];
      List<Card> otherCards =
        AllCards.Where(card => card.Rank != twoPairCards[0].Rank).ToList();
      Card? highestCardOfOtherCards =
        otherCards.OrderByDescending(card => card.Score).FirstOrDefault();

      Cards = twoPairCards;
      if (highestCardOfOtherCards != null)
        twoPairCards.Add(highestCardOfOtherCards);
    }
    // pair
    else if (
      groupedByRankCards.Any(group => group.Count() == 2)
    )
    {
      List<Card> pairCards =
       [.. groupedByRankCards.Where(group => group.Count() == 2).ToList()[0]];
      List<Card> otherCards =
        AllCards.Where(card => card.Rank != pairCards[0].Rank).ToList();
      otherCards.Sort((card1, card2) => card1.Score.CompareTo(card2.Score));

      Cards = pairCards;
      Cards.Add(otherCards[^1]);
      Cards.Add(otherCards[^2]);
      Cards.Add(otherCards[^3]);
    }
    // highestCard
    else
    {
      List<Card> subArray = [];
      List<Card> allCardsCopy = [.. AllCards];
      allCardsCopy.Sort((card1, card2) => card1.Score.CompareTo(card2.Score));
      Cards = allCardsCopy.GetRange(2, 5);
      Cards.Reverse();
    }
  }

  public void DefineScore()
  {
    if (type != null)
    {
      int gameTypeValue = Pontuation.HAND_ADDER[type];
      int sumOfCards = Cards.Aggregate(0, (sum, card) => sum + card.Score);
      Score = gameTypeValue + sumOfCards;
    }
  }

  public override string ToString()
  {
    string str = $"\n\n----------------------\n";
    str += $"\nType: {type}\n";
    foreach (var card in Cards)
    {
      str += $"\n -> {card}";
    }
    str += $"\n\t>Score: {Score}";
    str += $"\n\n----------------------\n";
    return str;
  }
}