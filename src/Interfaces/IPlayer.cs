namespace TexasHoldem;

public interface IPlayer
{
  public string Name { get; }
  public void ReceiveCard(Card card);
}