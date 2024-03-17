namespace TexasHoldem;

public interface IHandIdentifier
{
  static abstract bool IsStraight(IEnumerable<IGrouping<int, Card>> scoreGroupedCards);
  static abstract string IdentifyPlayerHand(List<Card> tableCards, List<Card> playerCards);
};