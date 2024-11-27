public static class HangmanGraphics
{
    private static readonly string[] HangmanStages = new string[]
    {
        @"
  -----
      |
      |
      |
      |
      |
     ===
        ", // 8 försök
        @"
  -----
  |   |
      |
      |
      |
      |
     ===
        ", // 7 försök
        @"
  -----
  |   |
  O   |
      |
      |
      |
     ===
        ", // 6 försök
        @"
  -----
  |   |
  O   |
  |   |
      |
      |
     ===
        ", // 5 försök
        @"
  -----
  |   |
  O   |
 /|   |
      |
      |
     ===
        ", // 4 försök
        @"
  -----
  |   |
  O   |
 /|\  |
      |
      |
     ===
        ", // 3 försök
        @"
  -----
  |   |
  O   |
 /|\  |
 /    |
      |
     ===
        ", // 2 försök
        @"
  -----
  |   |
  O   |
 /|\  |
 / \  |
      |
     ===
        "  // 1 försök
    };

    public static string GetHangmanFigure(int remainingAttempts)
    {
        // Hämta rätt steg beroende på antal kvarvarande försök
        int index = Math.Max(0, 8 - remainingAttempts);
        return HangmanStages[index];
    }
}
