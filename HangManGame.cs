using Spectre.Console;

public class HangmanGame
{
    private readonly IWordProvider _wordProvider;
    private string _currentWord;
    private HashSet<char> _guessedLetters;
    private int _remainingAttempts;

    public HangmanGame(IWordProvider wordProvider)
    {
        _wordProvider = wordProvider;
    }

    public void Run()
    {
        do
        {
            InitializeGame();
            PlayGame();
        } while (AskToPlayAgain());
    }

    private void InitializeGame()
    {
        _currentWord = _wordProvider.GetRandomWord();
        _guessedLetters = new HashSet<char>();
        _remainingAttempts = 8;  // Antalet försök

        // Rensa skärmen enbart när spelet startar
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("HANGMAN").Centered().Color(Color.Aqua));
        AnsiConsole.MarkupLine("[bold yellow]Let's begin![/]");
        AnsiConsole.MarkupLine("");
    }

    private void PlayGame()
    {
        while (_remainingAttempts > 0 && !IsGameWon())
        {
            DisplayGameState();
            var guess = GetPlayerGuess();

            if (!IsValidInput(guess))
            {
                DisplayError("Invalid input. Please enter a single letter.");
                continue;
            }

            if (_guessedLetters.Contains(guess))
            {
                DisplayError("You already guessed that letter.");
                continue;
            }

            _guessedLetters.Add(guess);

            if (_currentWord.Contains(guess))
            {
                AnsiConsole.MarkupLine($"[bold green]Correct! The word contains the letter '{guess}'.[/]");
            }
            else
            {
                DisplayError($"Incorrect! The word does not contain the letter '{guess}'.");
                _remainingAttempts--;
            }
        }

        DisplayEndGame();
    }

    private void DisplayGameState()
    {
        // Visa ordet med rätt gissade bokstäver och "_"
        var wordDisplay = string.Join(' ', _currentWord.Select(c => _guessedLetters.Contains(c) ? c : '_'));
        AnsiConsole.MarkupLine($"[bold yellow]Word:[/] {wordDisplay}");

        // Visa gissade bokstäver
        AnsiConsole.MarkupLine($"[bold cyan]Guessed Letters:[/] {string.Join(", ", _guessedLetters)}");

        // Visa återstående försök
        AnsiConsole.MarkupLine($"[bold yellow]Remaining Attempts:[/] {_remainingAttempts}");
        AnsiConsole.MarkupLine(""); // En tom rad för separation

        // Visa gubben i rätt stadium (bygg den för varje felaktig gissning)
        AnsiConsole.MarkupLine(HangmanGraphics.GetHangmanFigure(_remainingAttempts));
    }

    private char GetPlayerGuess()
    {
        AnsiConsole.Markup("[bold yellow]Enter a letter: [/]");
        char guess = Console.ReadKey().KeyChar.ToString().ToLower()[0];
        Console.WriteLine(); 
        return guess;
    }

    private bool IsValidInput(char input)
    {
        return char.IsLetter(input);
    }

    private void DisplayError(string message)
    {
        AnsiConsole.MarkupLine($"[bold red]{message}[/]");
    }

    private void DisplayEndGame()
    {
        if (IsGameWon())
        {
            AnsiConsole.MarkupLine($"[bold green]Congratulations! You guessed the word: {_currentWord}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Game Over![/]");
            AnsiConsole.MarkupLine($"[bold yellow]The word was: {_currentWord}[/]");
        }
    }

    private bool AskToPlayAgain()
    {
        AnsiConsole.Markup("[bold yellow]Do you want to play again? (y/n): [/]");
        var response = Console.ReadKey().KeyChar;
        Console.WriteLine(); 
        return response == 'y' || response == 'Y';
    }

    private bool IsGameWon() // metod för att kolla om spelet är vunnet
    {
        // Spelet är vunnit om alla bokstäver i ordet är korrekt gissade
        return _currentWord.All(c => _guessedLetters.Contains(c));
    }
}
