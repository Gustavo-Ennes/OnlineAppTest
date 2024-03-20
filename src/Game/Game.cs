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

  public void Execute(List<string> playerNames)
  {
    Deck deck = new();
    Table table = new();
    Dealer dealer = new(deck, table);
    // Polymorphism
    List<Player> players =
      [
        new RealPlayer(playerNames[0]),
        .. Enumerable.Range(1, 4).Select(i => new FakePlayer(playerNames[i])).ToList()
      ];

    Console.WriteLine(Color.ColorizeString("Introducing the players...", "cyan"));
    Thread.Sleep(500);
    IntroducePlayers(playerNames);
    Thread.Sleep(500);
    Console.WriteLine(Color.ColorizeString("Distributing cards to players...", "cyan"));
    DistributeCardsToPlayers(dealer, players);
    Thread.Sleep(500);
    Console.WriteLine(Color.ColorizeString("Putting cards on the table...", "cyan"));
    DistributeCardsOnTable(dealer, table);
    Thread.Sleep(500);
    Console.WriteLine(Color.ColorizeString("finishing the game...", "cyan"));
    FinishGame(dealer, players);
  }

  public void IntroducePlayers(List<string> playerNames)
  {
    string str = $"\n\n--------{Color.ColorizeString("Players in game", "cyan")}----------";
    foreach (var playerName in playerNames)
    {
      str += $"\n-> {Color.ColorizeString(playerName, "yellow")}";
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
    dealer.BurnACard();
    table.ReceiveTurn(dealer.GetCardsFromDeck(1)[0]);

    //river
    dealer.BurnACard();
    table.ReceiveRiver(dealer.GetCardsFromDeck(1)[0]);
    table.ListTableCards();
    Thread.Sleep(500);
  }

  public void FinishGame(Dealer dealer, List<Player> players)
  {
    dealer.FindAWinner(players);
  }
}