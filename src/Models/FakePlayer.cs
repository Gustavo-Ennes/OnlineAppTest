namespace TexasHoldem;

public class FakePlayer(string name) : Player(name)
{
  override public string ToString()
  {
    string str = $"\n\n----- {Color.ColorizeString(Name, "cyan")} ------";
    str += $"\nPocket cards: {cards[0]} {cards[1]}";
    str += $"\n\n{hand}";

    return str;
  }
}