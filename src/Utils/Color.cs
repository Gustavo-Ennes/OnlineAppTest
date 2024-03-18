namespace TexasHoldem;

public class Color : IColor
{
  // Foreground colors
  public static string BlackForeground { get; } = "\u001b[30m";
  public static string RedForeground { get; } = "\u001b[31m";
  public static string GreenForeground { get; } = "\u001b[32m";
  public static string YellowForeground { get; } = "\u001b[33m";
  public static string BlueForeground { get; } = "\u001b[34m";
  public static string MagentaForeground { get; } = "\u001b[35m";
  public static string CyanForeground { get; } = "\u001b[36m";
  public static string WhiteForeground { get; } = "\u001b[37m";

  // Background colors
  public static string BlackBackground { get; } = "\u001b[40m";
  public static string RedBackground { get; } = "\u001b[41m";
  public static string GreenBackground { get; } = "\u001b[42m";
  public static string YellowBackground { get; } = "\u001b[43m";
  public static string BlueBackground { get; } = "\u001b[44m";
  public static string MagentaBackground { get; } = "\u001b[45m";
  public static string CyanBackground { get; } = "\u001b[46m";
  public static string WhiteBackground { get; } = "\u001b[47m";

  // Reset code
  public static string ResetColors { get; } = "\u001b[0m";
  public static string ColorizeString(string str, string foreground, string? background = "")
  {
    string returnString = background switch
    {
      "black" => $"{BlackBackground}",
      "red" => $"{RedBackground}",
      "green" => $"{GreenBackground}",
      "yellow" => $"{YellowBackground}",
      "blue" => $"{BlueBackground}",
      "magenta" => $"{MagentaBackground}",
      "cyan" => $"{CyanBackground}",
      "white" => $"{WhiteForeground}",
      _ => ""
    };
    returnString += foreground switch
    {
      "black" => $"{BlackForeground}",
      "red" => $"{RedForeground}",
      "green" => $"{GreenForeground}",
      "yellow" => $"{YellowForeground}",
      "blue" => $"{BlueForeground}",
      "magenta" => $"{MagentaForeground}",
      "cyan" => $"{CyanForeground}",
      "white" => $"{WhiteBackground}",
      _ => ""
    };
    returnString += $"{str}{ResetColors}";
    return returnString;
  }

}