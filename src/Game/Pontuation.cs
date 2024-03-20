namespace TexasHoldem;

public class Pontuation : IPontuation
{
  public static readonly Dictionary<string, int> HAND_ADDER = new()
  {
    {"highCard", 7000},
    {"pair", 200000},
    {"twoPair", 300000},
    {"threeOfAKind", 400000},
    {"straight", 500000},
    {"flush", 600000},
    {"fullHouse",700000},
    {"fourOfAKind", 800000},
    {"straightFlush", 900000},
  };

  public static int CalculatePontuation(Hand hand)
  {
    return hand.type switch
    {
      "straightFlush" => CalculateStraightFlush(hand),
      "fourOfAKind" => CalculateFourOfAKind(hand),
      "fullHouse" => CalculateFullHouse(hand),
      "flush" => CalculateFlush(hand),
      "straight" => CalculateStraight(hand),
      "threeOfAKind" => CalculateThreeOfAKind(hand),
      "twoPair" => CalculateTwoPair(hand),
      "pair" => CalculatePair(hand),
      _ => CalculateHighCard(hand)
    };
  }

  public static int CalculateFlush(Hand hand)
  {
    hand.Cards.Sort((c1, c2) => c2.Score.CompareTo(c1.Score));
    Card strogerCard = hand.Cards.First();
    List<Card> otherCards = hand.Cards.GetRange(1, 4);
    int multiplier = 1000;
    return otherCards.Aggregate(
      HAND_ADDER["flush"] + (strogerCard.Score * multiplier * 3),
      (sum, card) =>
      {
        sum += card.Score * multiplier;
        multiplier /= 10;
        return sum;
      }
    );
  }

  public static int CalculateFourOfAKind(Hand hand)
  {
    IEnumerable<IGrouping<int, Card>> groupedByScore = hand.Cards.GroupBy(card => card.Score);
    Card? fourOfAKindCard = groupedByScore.FirstOrDefault(group => group.Count() == 4)?.FirstOrDefault();
    Card otherCardExceptFourOfAKindCard = groupedByScore
      .Where(group => group.Count() == 1)
      .Select(group => group.First())
      .ToList()
      .First();
    if (fourOfAKindCard != null)
      return HAND_ADDER["fourOfAKind"] + (fourOfAKindCard.Score * 10) + otherCardExceptFourOfAKindCard.Score;
    return HAND_ADDER["fourOfAKind"];
  }

  public static int CalculateFullHouse(Hand hand)
  {
    IEnumerable<IGrouping<int, Card>> groupedByScore = hand.Cards.GroupBy(card => card.Score);
    Card? fullHouseThreeOfAKindCard = groupedByScore.FirstOrDefault(group => group.Count() == 3)?.FirstOrDefault();
    Card fullHousePairCard = groupedByScore
      .Where(group => group.Count() == 2)
      .Select(group => group.First())
      .ToList()
      .First();
    if (fullHouseThreeOfAKindCard != null)
      return HAND_ADDER["fullHouse"] + (fullHouseThreeOfAKindCard.Score * 10) + fullHousePairCard.Score;
    return HAND_ADDER["fullHouse"];
  }

  public static int CalculateHighCard(Hand hand)
  {
    int multiplier = 10000;
    hand.Cards.Sort((c1, c2) => c2.Score.CompareTo(c1.Score));
    return hand.Cards
      .Aggregate(0, (sum, card) =>
      {
        sum += card.Score * multiplier;
        multiplier /= 10;
        return sum;
      }
    );
  }

  public static int CalculatePair(Hand hand)
  {
    IEnumerable<IGrouping<int, Card>> groupedByScore = hand.Cards.GroupBy(card => card.Score);
    Card? pairKindCard = groupedByScore.FirstOrDefault(group => group.Count() == 2)?.FirstOrDefault();
    List<Card> otherCardExceptPair = groupedByScore
      .Where(group => group.Count() == 1)
      .Select(group => group.First())
      .ToList();
    int multiplier = 1000;

    otherCardExceptPair.Sort((card1, card2) => card2.Score.CompareTo(card1.Score));

    if (pairKindCard != null)
      return otherCardExceptPair.Aggregate(
        HAND_ADDER["pair"] + (pairKindCard.Score * multiplier * 3),
        (sum, otherCard) =>
        {
          sum += otherCard.Score * multiplier;
          multiplier /= 10;
          return sum;
        }
    );
    return HAND_ADDER["pair"];
  }

  public static int CalculateStraight(Hand hand)
  {
    hand.Cards.Sort((card1, card2) => card1.Score.CompareTo(card2.Score));
    return HAND_ADDER["straight"] + hand.Cards[0].Score;
  }

  public static int CalculateStraightFlush(Hand hand)
  {
    hand.Cards.Sort((card1, card2) => card2.Score.CompareTo(card1.Score));
    // if lowest straightFlush, get the 5 score, not 14(ace)
    // if not, just get the highest card score
    return hand.Cards[0].Rank == "Ace" && hand.Cards[1].Rank == "5"
      ? HAND_ADDER["straightFlush"] + hand.Cards[1].Score
      : HAND_ADDER["straightFlush"] + hand.Cards[0].Score;
  }

  public static int CalculateThreeOfAKind(Hand hand)
  {
    IEnumerable<IGrouping<int, Card>> groupedByScore = hand.Cards.GroupBy(card => card.Score);
    Card? threeOfAKindCard = groupedByScore.FirstOrDefault(group => group.Count() == 3)?.FirstOrDefault();
    int multiplier = 1000;
    List<Card> otherCardExceptThreeOfAKindCards = groupedByScore
      .Where(group => group.Count() == 1)
      .Select(group => group.First())
      .ToList();

    otherCardExceptThreeOfAKindCards.Sort((card1, card2) => card1.Score.CompareTo(card2.Score));

    if (threeOfAKindCard != null)
      return otherCardExceptThreeOfAKindCards.Aggregate(
        HAND_ADDER["threeOfAKind"] + (threeOfAKindCard.Score * multiplier * 3),
        (sum, otherCard) =>
          {
            sum += otherCard.Score * multiplier;
            multiplier /= 10;
            return sum;
          }
      );
    return HAND_ADDER["threeOfAKind"];
  }

  public static int CalculateTwoPair(Hand hand)
  {
    IEnumerable<IGrouping<int, Card>> groupedByScore = hand.Cards.GroupBy(card => card.Score);
    Card? twoPairHighestPair = groupedByScore
      .Where(group => group.Count() == 2)
      .Select(group => group.OrderByDescending(card => card.Score).First())
      .First();
    Card? twoPairSecondPair = groupedByScore
      .Where(group => group.Count() == 2)
      .OrderByDescending(group => group.Key)
      .Skip(1)
      .Select(group => group.OrderByDescending(card => card.Score).First())
      .First();

    Card otherCardExceptTwoPairCards = groupedByScore
      .Where(group => group.Count() == 1)
      .Select(group => group.First())
      .ToList()
      .First();

    return HAND_ADDER["twoPair"]
      + (twoPairHighestPair.Score * 100)
      + (twoPairSecondPair.Score * 10)
      + otherCardExceptTwoPairCards.Score;

  }
}