using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a variable name (must start with a letter, max length 10, letters and digits only):");
        string userInput = Console.ReadLine();

        // Regular expression pattern for the specified condition
        string pattern = @"^[A-Za-z][A-Za-z0-9]{0,9}$";

        if (Regex.IsMatch(userInput, pattern))
        {
            Console.WriteLine($"'{userInput}' is a valid variable name.");
        }
        else
        {
            Console.WriteLine($"'{userInput}' is NOT a valid variable name.");
        }
    }
}
