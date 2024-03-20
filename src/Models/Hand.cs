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
        "Two player cards and five table cards is needed to initialize a hand."
    );

    type = HandIdentifier.IdentifyPlayerHand(tableCards, playerCards);
    BuildHand();
    DefineScore();
  }

  public void BuildHighCardHand()
  {
    List<Card> subArray = [];
    List<Card> allCardsCopy = [.. AllCards];
    allCardsCopy.Sort((card1, card2) => card2.Score.CompareTo(card1.Score));
    Cards = allCardsCopy.GetRange(0, 5);
    Cards.Reverse();
  }

  public void BuildPairHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards)
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

  public void BuildTwoPairHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards)
  {
    List<Card> firstPair = [.. groupedByRankCards.Where(group => group.Count() == 2).ToList()[0]];
    List<Card> secondPair = [.. groupedByRankCards.Where(group => group.Count() == 2).ToList()[1]];
    List<Card> twoPairCards =
      firstPair[0].Score < secondPair[0].Score ?
        [.. secondPair, .. firstPair] :
        [.. firstPair, .. secondPair];

    List<Card> otherCards =
      AllCards.Where(
        card =>
          card.Rank != twoPairCards[0].Rank && card.Rank != twoPairCards[2].Rank
      ).ToList();
    Card? highestCardOfOtherCards =
      otherCards.OrderByDescending(card => card.Score).FirstOrDefault();

    Cards = twoPairCards;
    if (highestCardOfOtherCards != null)
      twoPairCards.Add(highestCardOfOtherCards);
  }

  public void BuildThreeOfAKindHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards)
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

  public void BuildStraightHand()
  {
    AllCards.Sort((card1, card2) => card2.Score.CompareTo(card1.Score));
    List<Card> straight = [];
    foreach (Card card in AllCards)
    {
      int index = AllCards.IndexOf(card);
      // cards sorted, more easy
      // if no cards in straight I add one
      if (straight.Count == 0)
      {
        straight.Add(card);
        continue;
      }
      // and see if the next card has the score plus 1 or minus 1(sorted)
      if (card.Score == straight[0].Score + 1)
        straight.Insert(0, card);
      else if (card.Score == straight[^1].Score - 1)
        straight.Add(card);
      // if no connection made, I change only card on array
      if (straight.Count == 1)
        straight = [card];
    }

    if (straight.Count == 4 && straight[0].Score == 5 && AllCards.Any(card => card.Rank == "Ace"))
    {
      Card? ace = AllCards.Find(card => card.Rank == "Ace");
      if (ace != null)
        straight.Insert(0, ace);
    }
    Cards = straight;
  }

  public void BuildFlushHand(IEnumerable<IGrouping<string, Card>> groupedBySuitCards)
  {
    Cards =
     [.. groupedBySuitCards.Where(group => group.Count() == 5).ToList()[0]];
  }

  public void BuildFullHouseHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards)
  {
    List<Card> threeEqualCards =
        [.. groupedByRankCards.Where(group => group.Count() == 3).ToList()[0]];
    List<Card> pairCards =
      [.. groupedByRankCards.Where(group => group.Count() == 2).ToList()[0]];
    Cards = [.. threeEqualCards, .. pairCards];
  }

  public void BuildFourOfAKindHand(IEnumerable<IGrouping<string, Card>> groupedByRankCards)
  {
    List<Card> fourOfAKindCards =
       [.. groupedByRankCards.Where(group => group.Count() == 4).ToList()[0]];
    List<Card> otherCards = AllCards.Where(card => card.Rank != fourOfAKindCards[0].Rank).ToList();
    Card? highestCardOfOtherCards =
      otherCards.OrderByDescending(card => card.Score).FirstOrDefault();

    Cards = fourOfAKindCards;
    if (highestCardOfOtherCards != null)
    {
      Cards.Add(highestCardOfOtherCards);
    }
  }

  public void BuildStraightFlushHand()
  {
    AllCards.Sort((card1, card2) => card2.Score.CompareTo(card1.Score));
    List<Card> straightFlush = [];
    foreach (Card card in AllCards)
    {
      int index = AllCards.IndexOf(card);
      // cards sorted, more easy
      // if no cards in straight I add one
      if (straightFlush.Count == 0)
      {
        straightFlush.Add(card);
        continue;
      }
      // and see if the next card has the score plus 1 or minus 1(sorted)
      if (card.Score == straightFlush[0].Score + 1 && card.Suit == straightFlush[0].Suit)
        straightFlush.Insert(0, card);
      else if (card.Score == straightFlush[^1].Score - 1 && card.Suit == straightFlush[^1].Suit)
        straightFlush.Add(card);
      // if no connection made, I change only card on array
      if (straightFlush.Count == 1)
        straightFlush = [card];
    }

    if (straightFlush.Count == 4 && straightFlush[0].Score == 5 && AllCards.Any(card => card.Rank == "Ace"))
    {
      Card? ace = AllCards.Find(card => card.Rank == "Ace" && card.Suit == straightFlush[0].Suit);
      if (ace != null)
        straightFlush.Insert(0, ace);
    }
    Cards = straightFlush;
  }

  public void BuildHand()
  {
    IEnumerable<IGrouping<string, Card>> groupedByRankCards = AllCards.GroupBy(card => card.Rank);
    IEnumerable<IGrouping<string, Card>> groupedBySuitCards = AllCards.GroupBy(card => card.Suit);
    IEnumerable<IGrouping<int, Card>> groupedByScoreCards = AllCards.GroupBy(card => card.Score);

    //straightFlush
    if (
      HandIdentifier.IsStraight(groupedByScoreCards)
      && groupedBySuitCards.Any(group => group.Count() >= 5)
    )
      BuildStraightFlushHand();
    //fourOfAKind
    else if (groupedByRankCards.Any(group => group.Count() == 4))
      BuildFourOfAKindHand(groupedByRankCards);
    //fullHouse
    else if (
      groupedByRankCards.Any(group => group.Count() == 3)
      && groupedByRankCards.Any(group => group.Count() == 2)
    )
      BuildFullHouseHand(groupedByRankCards);
    //flush
    else if (
      groupedBySuitCards.Any(group => group.Count() == 5)
    )
      BuildFlushHand(groupedBySuitCards);
    // straight
    else if (
      HandIdentifier.IsStraight(groupedByScoreCards)
    )
      BuildStraightHand();
    //threeOfAKind
    else if (
      groupedByRankCards.Any(group => group.Count() == 3)
    )
      BuildThreeOfAKindHand(groupedByRankCards);
    //two pair
    else if (
      groupedByRankCards.Count(group => group.Count() == 2) >= 2
    )
      BuildTwoPairHand(groupedByRankCards);
    // pair
    else if (
      groupedByRankCards.Any(group => group.Count() == 2)
    )
      BuildPairHand(groupedByRankCards);
    // highestCard
    else
      BuildHighCardHand();
  }

  public void DefineScore()
  {
    if (type != null)
    {
      Score = Pontuation.CalculatePontuation(this);
    }
  }

  public override string ToString()
  {
    string str = $"\nType: {Color.ColorizeString(type ?? "", "cyan")}\n";
    foreach (var card in Cards)
    {
      str += $"\n -> {card}";
    }
    str += $"\n\t>{Color.ColorizeString("Score", "red")}: {Score}";
    str += $"\n\n----------------------\n";
    return str;
  }
}