namespace TexasHoldem;

class Program
{
    private static readonly HttpClient httpClient = new();
    private static NameAPIService? NameAPIService;
    static async Task Main(string[] args)
    {
        Game game = Game.GetInstance();
        ConsoleKeyInfo keyInfo;
        NameAPIService = new(httpClient);

        Console.WriteLine("~ Texas Hold'em 6-MAX all-in app ~");

        Console.WriteLine("Tell me your name: ");
        string? input = Console.ReadLine();
        string mainPlayerName = string.IsNullOrWhiteSpace(input) ? "John Doe" : input;

        do
        {
            List<string> playerNames = [mainPlayerName];
            // fake name api calls
            Task task = Task.Run(async () =>
            {
                List<string> otherPlayerNames = await NameAPIService.GetPlayerNames();
                playerNames = [mainPlayerName, ..otherPlayerNames];
            });
            await task;

            Console.WriteLine("\nContinuing...");
            game.Execute(playerNames);

            Console.WriteLine("\nYou can play again with Enter or ESC to exit.");
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Exiting...");
                return;
            }
            
            while(keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nInvalid key. Hit Enter to continue or ESC to exit");
                keyInfo = Console.ReadKey();
            }
        } while (true);
    }
}
