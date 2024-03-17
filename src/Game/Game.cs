namespace TexasHoldem;

// singleton pattern
public class Game : IGame
{
  private static Game? instance;

  private Game() { }

  public static Game GetInstance()
  {
    instance ??= new Game();
    return instance;
  }

  public void Execute()
  {
    Deck deck = new();
    Table table = new();
    Dealer dealer = new(deck, table);
    List<string> playerNames =
      ["John Petrucci", "Steve Vai", "Mike Portnoy", "Joe Duplantier", "Mario Duplantier"];
    List<Player> players = Enumerable.Range(0, 5).Select(i => new Player(playerNames[i])).ToList();

    Console.WriteLine("Introducing the players...");
    Thread.Sleep(500);
    IntroducePlayers(playerNames);
    Thread.Sleep(500);
    Console.WriteLine("Distributing cards to players...");
    DistributeCardsToPlayers(dealer, players);
    Thread.Sleep(500);
    Console.WriteLine("Putting cards on the table...");
    DistributeCardsOnTable(dealer, table);
    Thread.Sleep(500);
    Console.WriteLine("finishing the game...");
    FinishGame(dealer, players);
  }

  public void IntroducePlayers(List<string> playerNames)
  {
    string str = "\n\n--------------------------";
    str += "   \nPlayers in game";
    foreach (var playerName in playerNames)
    {
      str += $"\n-> {playerName}";
    }
    str += "\n\n--------------------------";

    Console.WriteLine(str);
  }

  public void DistributeCardsToPlayers(Dealer dealer, List<Player> players)
  {
    // give one card to each twice
    foreach (var _ in Enumerable.Range(0, 2))
    {
      foreach (Player player in players)
      {
        dealer.DistributeACardTo(player);
      }

    }
  }

  public void DistributeCardsOnTable(Dealer dealer, Table table)
  {
    // flop
    dealer.BurnACard();
    List<Card> flopCards = dealer.GetCardsFromDeck(3);
    table.ReceiveFlop(flopCards);

    // turn
    table.ReceiveTurn(dealer.GetCardsFromDeck(1)[0]);

    //river
    table.ReceiveRiver(dealer.GetCardsFromDeck(1)[0]);
  }

  public void FinishGame(Dealer dealer, List<Player> players)
  {
    dealer.FindAWinner(players);
  }
}