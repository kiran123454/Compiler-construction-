using System;
using System.Text.RegularExpressions;

public class PasswordChecker
{
    public static bool IsValidPassword(string password, string regNumber, string name)
    {
        // 1. 2 Characters of Registration Number
        string regChars = regNumber.Substring(0, 2);
        if (!password.Contains(regChars)) return false;

        // 2. At Least One Uppercase Alphabet
        if (!Regex.IsMatch(password, "[A-Z]")) return false;

        // 3. At Least 2 Special Characters in Order
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+{}\[\]:;<>,.?~`]{2}")) return false;

        // 4. 4 Lowercase Alphabets from Name
        int foundCount = 0;
        foreach (char c in name.ToLower())
        {
            if (char.IsLetter(c) && password.Contains(c))
            {
                foundCount++;
            }
        }
        if (foundCount < 4) return false;

        // 5. Max Length 12 Characters
        if (password.Length > 12) return false;

        return true;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter password:");
        string password = Console.ReadLine();
        Console.WriteLine("Enter registration number:");
        string regNumber = Console.ReadLine();
        Console.WriteLine("Enter name:");
        string name = Console.ReadLine();

        if (IsValidPassword(password, regNumber, name))
        {
            Console.WriteLine("Valid password.");
        }
        else
        {
            Console.WriteLine("Invalid password.");
        }
    }
}