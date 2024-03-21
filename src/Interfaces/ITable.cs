namespace TexasHoldem;

public interface ITable
{
  public void ReceiveFlop(List<Card> flopCards);
  public void ReceiveTurn(Card turnCard);
  public void ReceiveRiver(Card riverCard);
}