using Spectre.Console;
using Figgle;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Visa titelskärm
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("HANGMAN")
                    .Centered()
                    .Color(Color.Aqua));

            AnsiConsole.MarkupLine("[bold yellow]Welcome to Hangman![/]");
            AnsiConsole.MarkupLine("[italic grey]Try to guess the hidden word one letter at a time![/]");
            AnsiConsole.MarkupLine("[green]Press any key to start...[/]");
            Console.ReadKey();

            // Starta spelet
            var wordProvider = new WordProvider("words.json");
            var hangmanGame = new HangmanGame(wordProvider);
            hangmanGame.Run();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]An unexpected error occurred:[/] {ex.Message}");
        }
    }
}
