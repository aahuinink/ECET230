using CalculatorLibrary;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            CalculatorProcess calculator = new CalculatorProcess();

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

            while (!endApp)
            {
                // Ask the user to type the first number.
                calculator.num1 = calculator.GetNumber(1);

                // get the user to enter the second number
                calculator.num2 = calculator.GetNumber(2);

                // Ask the user to choose an operator.
                calculator.operID = calculator.GetOperatorID();

                try
                {
                    // try to do the operation
                    calculator.DoOperation();

                    // if the result is not a number
                    if (double.IsNaN(calculator.result))
                    {
                        // alert the user
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        // otherwise print out the result in the colour green
                        Console.WriteLine($"Your result:");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{calculator.num1} {calculator.oper} {calculator.num2} = {calculator.result}");
                        Console.ResetColor();
                    }
                }
                // if theres an error/exception
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message); // print out the error
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                endApp = (Console.ReadLine() == "n" ? true : false); // check to see if the user wants to exit the application

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}