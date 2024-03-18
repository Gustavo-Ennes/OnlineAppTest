namespace TexasHoldem;

public interface IColor
{
  public abstract static string ColorizeString(string str, string foreground, string? background);
}