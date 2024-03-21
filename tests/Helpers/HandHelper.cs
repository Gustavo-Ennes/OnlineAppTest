namespace TexasHoldemTests;

using TexasHoldem;

public class HandHelper
{
  public static Hand GetHandByNotation(string notation)
  {
    List<List<char>> cardNotations = SeparateCardsNotations(notation);
    List<Card> cards = GetCardsByNotation(cardNotations);
    List<Card> playerCards = cards.GetRange(0, 2);
    List<Card> tableCards = cards.GetRange(2, 5);

    return new(tableCards, playerCards);
  }

  public static string GetSuitBySuitNotation(char suitNotation)
  {
    return suitNotation switch
    {
      'h' => "Hearts",
      'c' => "Clubs",
      'd' => "Diamonds",
      's' => "Spades",
      _ => ""
    };
  }

  public static List<Card> GetCardsByNotation(List<List<char>> cardsNotations)
  {
    List<Card> cards = [];
    static string ParseNotationRank(char c) => c switch
    {
      'T' => "10",
      'J' => "Jack",
      'Q' => "Queen",
      'K' => "King",
      'A' => "Ace",
      _ => c.ToString()
    };

    foreach (List<char> cardNotation in cardsNotations)
    {
      Card newCard = new(ParseNotationRank(cardNotation[0]), GetSuitBySuitNotation(cardNotation[1]));
      cards.Add(newCard);
    }
    return cards;
  }

  public static List<List<char>> SeparateCardsNotations(string notation)
  {
    List<List<char>> cardsNotations = [];
    List<char> tempCardNotation = [];
    
    foreach (char c in notation)
    {
      if (tempCardNotation.Count == 2)
      {
        cardsNotations.Add(tempCardNotation);
        tempCardNotation = [];
      }
      if (tempCardNotation.Count < 2)
      {
        tempCardNotation.Add(c);
      }
    }
    cardsNotations.Add(tempCardNotation);
    return cardsNotations;
  }
}
