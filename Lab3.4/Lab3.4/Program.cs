using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = @"C:\ExampleDirectory\";

        Func<string, IEnumerable<string>> tokenizer = text =>
        {
            char[] delimiters = { ' ', ',', '.', ':', ';', '!', '?', '\t', '\n', '\r' };
            return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        };

        Func<IEnumerable<string>, IDictionary<string, int>> wordCounter = words =>
        {
            Dictionary<string, int> frequencies = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (frequencies.ContainsKey(word))
                {
                    frequencies[word]++;
                }
                else
                {
                    frequencies[word] = 1;
                }
            }

            return frequencies;
        };

        Action<IDictionary<string, int>> displayResults = frequencies =>
        {
            foreach (KeyValuePair<string, int> entry in frequencies.OrderByDescending(e => e.Value))
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        };

        string[] fileNames = Directory.GetFiles(directoryPath);

        foreach (string fileName in fileNames)
        {
            string text = File.ReadAllText(fileName);

            IEnumerable<string> tokens = tokenizer(text);

            IDictionary<string, int> frequencies = wordCounter(tokens);

            displayResults(frequencies);
        }
    }
}
