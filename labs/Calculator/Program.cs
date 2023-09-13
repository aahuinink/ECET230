

// Declare variables and then initialize to zero.
using System;

// declare variables and initialize to 0 or empty
double num1 = 0; double num2 = 0; string op = "";

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
op = Console.ReadLine();


// Wait for the user to respond before closing.
Console.Write("Press any key to close the Calculator console app...");
Console.ReadKey();
