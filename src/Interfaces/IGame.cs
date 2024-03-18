namespace TexasHoldem;

public interface IGame
{
  static abstract Game GetInstance();
  void Execute(List<string> playerNames);
  void IntroducePlayers(List<string> playerNames);
  void DistributeCardsToPlayers(Dealer dealer, List<Player> players);
  void DistributeCardsOnTable(Dealer dealer, Table table);  
  void FinishGame(Dealer dealer, List<Player> players);
}
