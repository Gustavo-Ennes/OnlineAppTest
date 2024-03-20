namespace TexasHoldem;

public class FakePlayer(string name) : Player(name)
{
  override public string ToString()
  {
    string str = $"\n\n----- {Color.ColorizeString(Name, "cyan")} ------";
    str += "\nCards:\n";
    str += $"\n -> {cards[0]}\n -> {cards[1]}";
    str += $"\n\n{hand}";

    return str;
  }
}