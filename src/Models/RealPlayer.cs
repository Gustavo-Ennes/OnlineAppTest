namespace TexasHoldem;

public class RealPlayer(string name) : Player(name)
{
  public int Score {get;} = 0;
  override public string ToString()
  {
    string str = $"\n\n----- {Color.ColorizeString("Y O U", "cyan")} ------";
    str += "\nPocket cards:\n";
    str += $"\n -> {cards[0]}\n -> {cards[1]}";
    str += $"\n\n{hand}";

    return str;
  }
}