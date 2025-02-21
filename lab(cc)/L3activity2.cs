using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string to check for keywords (int, float, double, char):");
        string userInput = Console.ReadLine();

        // Regular expression to match specific keywords
        string pattern = @"\b(int|float|double|char)\b";

        MatchCollection matches = Regex.Matches(userInput, pattern);

        if (matches.Count > 0)
        {
            Console.WriteLine("\nFound keywords:");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);
            }
        }
        else
        {
            Console.WriteLine("\nNo keywords found.");
        }
    }
}
