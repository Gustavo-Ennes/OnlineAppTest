namespace TexasHoldem;

public interface IDealer{
  public Deck Deck { get; }
  public Table Table { get; }
  public void DistributeACardTo(Player player);
  public List<Card> GetCardsFromDeck(int quantity);
  public void BurnACard();
  public void LogPlayerGame(Player player);
  public void FindAWinner(List<Player> players);
}