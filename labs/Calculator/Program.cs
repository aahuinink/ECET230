

// Declare variables and then initialize to zero.
using System;

double num1 = 0; double num2 = 0;

// Display title as the C# console calculator app.
Console.WriteLine(@"
    _________                            .__                          
    \_   ___ \  ____   ____   __________ |  |   ____                  
    /    \  \/ /  _ \ /    \ /  ___/  _ \|  | _/ __ \                 
    \     \___(  <_> )   |  \\___ (  <_> )  |_\  ___/                 
     \______  /\____/|___|  /____  >____/|____/\___  >                
            \/            \/     \/                \/                 
    _________        .__               .__          __                
    \_   ___ \_____  |  |   ____  __ __|  | _____ _/  |_  ___________ 
    /    \  \/\__  \ |  | _/ ___\|  |  \  | \__  \\   __\/  _ \_  __ \
    \     \____/ __ \|  |_\  \___|  |  /  |__/ __ \|  | (  <_> )  | \/
     \______  (____  /____/\___  >____/|____(____  /__|  \____/|__|   
            \/     \/          \/                \/                  
   

By Aaron Huinink - C0520296
            ");

// Ask the user to type the first number.
Console.WriteLine("Type a number, and then press Enter");
Console.ForegroundColor = ConsoleColor.Green;
num1 = Convert.ToDouble(Console.ReadLine());
Console.ResetColor();

// Ask the user to type the second number.
Console.WriteLine("Type another number, and then press Enter");
Console.ForegroundColor = ConsoleColor.Green;
num2 = Convert.ToDouble(Console.ReadLine());
Console.ResetColor();

// Ask the user to choose an option.
Console.WriteLine("Choose an option from the following list:");
Console.WriteLine("\ta - Add");
Console.WriteLine("\ts - Subtract");
Console.WriteLine("\tm - Multiply");
Console.WriteLine("\td - Divide");
Console.Write("Your option? ");

// Use a switch statement to do the math.
switch (Console.ReadLine())
{
    case "a":
        Console.Write($"Your result: {num1} + {num2} = ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(num1 + num2);
        Console.ResetColor();
        break;
    case "s":
        Console.Write($"Your result: {num1} - {num2} = ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(num1 - num2);
        Console.ResetColor();
        break;
    case "m":
        Console.Write($"Your result: {num1} * {num2} = ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(num1 * num2);
        Console.ResetColor();
        break;
    case "d":
        while (num2 == 0)
        {
            Console.WriteLine("Please enter a non-zero divisor!");
            num2 = Convert.ToDouble(Console.ReadLine());
        }
        Console.Write($"Your result: {num1} / {num2} = ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(num1 / num2);
        Console.ResetColor();
        break;
}
// Wait for the user to respond before closing.
Console.Write("Press any key to close the Calculator console app...");
Console.ReadKey();