namespace TexasHoldem;

public interface IDeck
{
  // public List<Card> cards;
  public void Initialize();
  public void Shuffle();
  public List<Card> DealCards(int quantity);
  public void ListCards();
}