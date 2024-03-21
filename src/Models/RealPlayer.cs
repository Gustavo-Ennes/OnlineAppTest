namespace TexasHoldem;

public class RealPlayer(string name) : Player(name)
{
  public int Score {get; set;} = 0;
  override public string ToString()
  {
    string str = $"\n\n----- {Color.ColorizeString("Y O U", "cyan")} ------";
    str += $"\nScore until now: {Score}\n";
    str += $"\nPocket cards: {cards[0]} {cards[1]}";
    str += $"\n\n{hand}";

    return str;
  }
}