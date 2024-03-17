namespace TexasHoldem;

class Program
{
    static void Main(string[] args)
    {
        Game instance = Game.GetInstance();

        Console.WriteLine("Texas Hold'em all-in app. Hit Enter to continue or ESC to exit.");

        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(); // Read the key pressed by the user

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("\nContinuing...");
                instance.Execute();
                Console.WriteLine("\nYou can play again with Enter or ESC to exit.");
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("\n Exiting...");
                return;
            }
            else
            {
                Console.WriteLine("\nInvalid key. Hit Enter to continue or ESC to exit");
            }

        } while (true);
    }
}
