using System;
using System.Linq;

public class PasswordGenerator
{
    private static Random random = new Random();

    public static string GeneratePassword(string firstName, string lastName, string regNumber, string favMovie, string favFood)
    {
        string allChars = firstName + lastName + regNumber + favMovie + favFood;
        string shuffled = new string(allChars.ToCharArray().OrderBy(s => random.NextDouble()).ToArray());

        // Ensure at least one uppercase, one special, and 4 lowercase
        string password = "";
        password += shuffled.Where(char.IsUpper).FirstOrDefault(); // Uppercase
        password += shuffled.Where(char.IsLower).Take(4).ToArray(); // Lowercase
        password += shuffled.Where(ch => !char.IsLetterOrDigit(ch)).Take(2).ToArray(); // Special chars

        // Add remaining characters randomly until length 12
        int remainingLength = 12 - password.Length;
        password += shuffled.Substring(0, Math.Min(remainingLength, shuffled.Length));

        // Shuffle again for good measure
        return new string(password.ToCharArray().OrderBy(s => random.NextDouble()).ToArray());
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter last name:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter registration number (3 digits):");
        string regNumber = Console.ReadLine();
        Console.WriteLine("Enter favorite movie:");
        string favMovie = Console.ReadLine();
        Console.WriteLine("Enter favorite food:");
        string favFood = Console.ReadLine();

        string password = GeneratePassword(firstName, lastName, regNumber, favMovie, favFood);
        Console.WriteLine("Generated password: " + password);
    }
}