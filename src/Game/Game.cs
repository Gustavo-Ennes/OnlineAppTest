namespace TexasHoldem;

// singleton pattern
public class Game : IGame
{
  private static Game? instance;
  public List<Player> LastWinners { get; set; } = [];
  public int LastScore { get; set; } = 0;
  private Table? table;
  private Deck? deck;
  private Dealer? dealer;

  private Game() { }

  public static Game GetInstance()
  {
    instance ??= new Game();
    return instance;
  }

  public void Execute(List<string> playerNames)
  {
    table = new();
    deck = new();
    dealer = new(deck, table);
    // Polymorphism
    List<Player> players = [ new RealPlayer(playerNames[0])];
    List<string> fakePlayerNames = playerNames.GetRange(1, playerNames.Count - 1);
    
    foreach(string name in fakePlayerNames)
    {
      players.Add( new FakePlayer(name));
    }

    Console.WriteLine(Color.ColorizeString("Introducing the players...", "cyan"));
    Thread.Sleep(200);
    IntroducePlayers(playerNames);
    Thread.Sleep(200);
    Console.WriteLine(Color.ColorizeString("Distributing cards to players...", "cyan"));
    DistributeCardsToPlayers(dealer, players);
    Thread.Sleep(200);
    Console.WriteLine(Color.ColorizeString("\nPutting cards on the table...", "cyan"));
    DistributeCardsOnTable(dealer, table);
    Thread.Sleep(200);
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
    str += "\n\n--------------------------\n";

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
    Console.WriteLine($"\n\nTable Cards: {table}");
    Thread.Sleep(200);
  }

  public void DisplayWinners()
  {
    Thread.Sleep(200);
    string str = $"\n\n\n\n{Color.ColorizeString("-------------------------------------", "blue")}\n";
    Thread.Sleep(200);
    if (LastWinners.Count == 1)
    {
      str += $"     {Color.ColorizeString("W I N N E R", "red")}";
      Thread.Sleep(200);
      str += $"{LastWinners.First()}";
    }
    // sometimes more than 1 players have the strongest hand
    else
    {
      str += $"     {Color.ColorizeString("W I N N E R S", "red")}";
      str += $" {Color.ColorizeString("\n   - splitted pot hand -", "yellow")}";
      Thread.Sleep(200);
      foreach (Player winner in LastWinners)
      {
        str += $"{winner}";
      }
    }
    str += $"\nTable Cards: {table}";
    str += $"{Color.ColorizeString("-------------------------------------", "blue")}";
    Console.WriteLine(str);
  }

  public void FinishGame(Dealer dealer, List<Player> players)
  {
    List<Player> winners = dealer.FindTheWinners(players);

    // give real player score point to sum
    foreach (Player winner in winners)
    {
      if (winner is RealPlayer realWinner)
        realWinner.Score += realWinner.hand?.Score ?? 0;
    }
    // add instance last game info
    if (instance != null)
    {
      instance.LastScore = winners.First().hand?.Score ?? 0;
      instance.LastWinners = winners;
    }

    DisplayWinners();
  }
}