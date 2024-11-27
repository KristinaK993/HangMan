public class GameState
{
    public string Word { get; set; }
    public HashSet<char> CorrectGuesses { get; set; } = new();
    public HashSet<char> IncorrectGuesses { get; set; } = new();
    public int RemainingAttempts { get; set; } = 6;

    public GameState(string word)
    {
        Word = word;
    }
    public bool IsGameWon() => Word.All(c => CorrectGuesses.Contains(c));
    public bool IsGameOver() => RemainingAttempts <= 0;
}