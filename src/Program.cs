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

        Console.WriteLine("~ Texas Hold'em all-in app ~");

        Console.WriteLine("Tell me your name: ");
        string? nameInput = Console.ReadLine();
        string mainPlayerName = string.IsNullOrWhiteSpace(nameInput) ? "John Doe" : nameInput;

        do
        {
            Console.WriteLine("Choose a modality:\n1. Heads Up\n2. 6-max\n3. Full ring\n");
            string? modalityInput = Console.ReadLine();
            int? modalityNumber = null;
            while (modalityNumber == null)
            {
                try
                {
                    int parsed = int.Parse(modalityInput ?? "");
                    int[] allowedOptions = [1, 2, 3];
                    if (allowedOptions.Contains(parsed))
                    {
                        modalityNumber = parsed;
                        string modalityLabel = parsed switch
                        {
                            1 => "Heads-up",
                            2 => "6-max",
                            3 => "full-ring",
                            _ => ""
                        };
                        Console.WriteLine($"chosen modality: {modalityLabel}.\n");
                    }
                    else
                    {
                        Console.WriteLine($"{parsed} isn't a valid option. Digit a valid one:");
                        modalityInput = Console.ReadLine();
                    }
                }
                catch
                {
                    Console.WriteLine("This is not a number at all.");
                };
            }

            List<string> playerNames = [mainPlayerName];
            // fake name api calls
            Task task = Task.Run(async () =>
            {
                List<string> otherPlayerNames = await NameAPIService.GetPlayerNames((int)modalityNumber);
                playerNames = [mainPlayerName, .. otherPlayerNames];
            });
            await task;

            Console.WriteLine("\nContinuing...");
            game.Execute(playerNames);

            Console.WriteLine("\nYou can play again with Enter or ESC to exit.");
            keyInfo = Console.ReadKey();

            while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nInvalid key. Hit Enter to continue or ESC to exit");
                keyInfo = Console.ReadKey();
            }
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Exiting...");
                return;
            }

        } while (true);
    }
}
