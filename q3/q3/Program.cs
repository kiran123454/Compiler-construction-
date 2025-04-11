using System;
using System.Collections.Generic;

class SymbolTableEntry
{
    public string VariableName { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public int LineNumber { get; set; }

    public override string ToString()
    {
        return $"[Line {LineNumber}] {Type} {VariableName} = {Value}";
    }
}

class SymbolTable
{
    private List<SymbolTableEntry> entries = new List<SymbolTableEntry>();
    private int currentLineNumber = 1;

    // Custom palindrome checker for substrings of length ≥ 3
    public bool ContainsPalindromeSubstring(string name)
    {
        for (int i = 0; i <= name.Length - 3; i++)
        {
            for (int j = i + 2; j < name.Length; j++)
            {
                if (IsPalindrome(name, i, j))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool IsPalindrome(string s, int start, int end)
    {
        while (start < end)
        {
            if (s[start] != s[end])
            {
                return false;
            }
            start++;
            end--;
        }
        return true;
    }

    public bool TryAddEntry(string declaration)
    {
        // Parse the declaration (simple parsing for demonstration)
        var parts = declaration.Split(new[] { ' ', '=', ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 3) return false;

        string type = parts[0];
        string varName = parts[1];
        string value = parts[2];

        if (!ContainsPalindromeSubstring(varName))
        {
            return false;
        }

        entries.Add(new SymbolTableEntry
        {
            VariableName = varName,
            Type = type,
            Value = value,
            LineNumber = currentLineNumber
        });

        currentLineNumber++;
        return true;
    }

    public void PrintSymbolTable()
    {
        Console.WriteLine("\nSymbol Table Contents:");
        Console.WriteLine("----------------------");
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
        if (entries.Count == 0)
        {
            Console.WriteLine("(Empty)");
        }
        Console.WriteLine("----------------------");
    }
}

class Program
{
    static void Main()
    {
        SymbolTable symbolTable = new SymbolTable();

        Console.WriteLine("Symbol Table with Palindrome Check");
        Console.WriteLine("Enter variable declarations one at a time (e.g., 'int val33 = 999;')");
        Console.WriteLine("Enter 'exit' to finish\n");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine().Trim();

            if (input.ToLower() == "exit")
            {
                break;
            }

            if (symbolTable.TryAddEntry(input))
            {
                Console.WriteLine($"Added: {input}");
            }
            else
            {
                Console.WriteLine($"Rejected: Variable name must contain a palindrome substring of length ≥ 3");
            }
        }

        symbolTable.PrintSymbolTable();
    }
}