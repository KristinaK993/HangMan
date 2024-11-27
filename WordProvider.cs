using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class WordProvider : IWordProvider
{
    private readonly string _filePath;
    private readonly List<string> _words;

    public WordProvider(string filePath)
    {
        _filePath = filePath;
        _words = File.Exists(filePath)
            ? JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(filePath)) ?? new List<string>()
            : new List<string>();
    }

    public string GetRandomWord()
    {
        if (_words.Count == 0)
        {
            throw new Exception("No words available.");
        }

        var random = new Random();
        return _words[random.Next(_words.Count)];
    }
}
