namespace TexasHoldem;

public class Dealer : IDealer
{
  public Deck Deck { get; }
  public Table Table { get; }

  public Dealer(Deck deck, Table table)
  {
    Deck = deck;
    Table = table;
    Deck.Shuffle();
  }

  public void DistributeACardTo(Player player)
  {
    List<Card> dealedCards = Deck.DealCards();
    player.ReceiveCard(dealedCards[0]);
  }

  public List<Card> GetCardsFromDeck(int quantity)
  {
    return Deck.DealCards(quantity);
  }

  public void BurnACard()
  {
    Deck.DealCards();
  }

  public void LogPlayerGame(Player player)
  {
    Console.WriteLine($"\nplayer: {player.Name}");

    Console.WriteLine($"\nplayer cards: \n{player.cards[0]}\n{player.cards[1]}");
    Console.WriteLine($"\nTableCards:");
    foreach (var tableCard in Table.cards)
    {
      Console.WriteLine($"\n -> {tableCard}");
    }
    Console.WriteLine(
      $"Player hand type: {HandIdentifier.IdentifyPlayerHand(Table.cards, player.cards)}"
    );
    Thread.Sleep(500);
  }

  // find the best hand by group(two pair,straight, flush)
  // and then see if there's another player with such hand
  // if not, it's the winner
  // if yes, find the higher card by score
  public void FindAWinner(List<Player> players)
  {
    int? highestScore = 0;
    Player? winner = null;
    Hand? bestHand = null;

    foreach (var player in players)
    {
      string playerHandType = HandIdentifier.IdentifyPlayerHand(
        Table.cards,
        player.cards
      );
      Hand playerHand = new(Table.cards, player.cards);

      LogPlayerGame(player);
      Console.WriteLine(playerHand);

      if (playerHand.Score > highestScore)
      {
        highestScore = playerHand.Score;
        winner = player;
        bestHand = playerHand;
      }
    }
    if (winner != null)
    {
      Thread.Sleep(500);
      Console.WriteLine("\n\n--------------------------\n");
      Thread.Sleep(500);
      Console.WriteLine("\n     W I N N E R");
      Thread.Sleep(500);
      Console.WriteLine($"\n -> Player: {winner.Name}");
      Thread.Sleep(500);
      Console.WriteLine($"\n{bestHand}");
    }
  }
}