namespace TexasHoldem;

// singleton pattern
public class Game : IGame
{
  private static Game? instance;
  public List<Player> LastWinners { get; set; } = [];
  public int LastScore { get; set; } = 0;

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

  public void DisplayWinners()
  {
    Thread.Sleep(500);
    string str = "\n\n--------------------------\n";
    Thread.Sleep(500);
    if (LastWinners.Count == 1)
    {
      str += $"     {Color.ColorizeString("W I N N E R", "red")}";
      Thread.Sleep(500);
      str += $"{LastWinners.First()}";
    }
    // sometimes more than 1 players have the strongest hand
    else
    {
      str += $"     {Color.ColorizeString("W I N N E R S", "red")}";
      str += $" {Color.ColorizeString("- splitted pot hand -", "yellow")}";
      Thread.Sleep(500);
      foreach (Player winner in LastWinners)
      {
        str += $"{winner}";
      }
      str += $" {Color.ColorizeString("- splitted pot hand -", "yellow")}";
    }

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