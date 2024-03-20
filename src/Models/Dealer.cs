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
    Console.WriteLine($"distributing one card to {player.Name}.");
    Thread.Sleep(500);
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
    Console.WriteLine($"Dealer {Color.ColorizeString("burned", "red")} a card.");
    Thread.Sleep(500);
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

  public void FindAWinner(List<Player> players)
  {
    int? highestScore = 0;
    Player? winner = null;

    foreach (var player in players)
    {
      player.hand = new(Table.cards, player.cards);
      Console.WriteLine(player);
      Thread.Sleep(500);

      if (player.hand.Score > highestScore)
      {
        highestScore = player.hand.Score;
        winner = player;
      }
    }
    if (winner != null)
    {
      Thread.Sleep(500);
      Console.WriteLine("\n\n--------------------------\n");
      Thread.Sleep(500);
      Console.WriteLine($"     {Color.ColorizeString("W I N N E R", "red")}");
      Thread.Sleep(500);
      Console.WriteLine($"{winner}");
    }
  }
}