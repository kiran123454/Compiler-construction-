using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Prompt user for student ID
        Console.Write("Enter your student ID (e.g., BCS-023): ");
        string studentId = Console.ReadLine();

        // Extract last two digits from the ID
        string lastTwoDigits = Regex.Match(studentId, @"\d{2}(?=\D*$)").Value;

        // If we didn't find two digits at the end, use 00 as default
        if (lastTwoDigits.Length < 2)
        {
            lastTwoDigits = "00";
            Console.WriteLine("Couldn't find two digits at the end of ID. Using 00 as default.");
        }

        // Parse the digits to get x and y values
        int x = int.Parse(lastTwoDigits[0].ToString());
        int y = int.Parse(lastTwoDigits[1].ToString());
        int z = 4;

        // Calculate the result
        int result = (x * y) + z;

        // Display the output
        Console.WriteLine($"x = {x}");
        Console.WriteLine($"y = {y}");
        Console.WriteLine($"z = {z}");
        Console.WriteLine($"Result = {result}");
    }
}