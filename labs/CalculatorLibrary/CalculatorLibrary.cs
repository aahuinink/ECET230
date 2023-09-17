using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class CalculatorProcess
    {
        JsonWriter writer;      // for creating a log file
        public double num1;     // first inputed number
        public double num2;     // second inputted number
        public char operID;     // the operator identfier (a, s, m, etc...)
        public char oper;       // operator char (+, -, x, etc...)
        public double result;   // resultant output
        
        // Custom Constructor to also create the logfile
        public CalculatorProcess()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        // --- GetNumber --- // 
        /* 
         * Gets a number input from the user while handling non-numeric input errors
         * 
         * Args: 
         *      id<char>            :   The position id of the number, either 1st or 2nd,
         *    
         * Returns: 
         *      cleanNum<double>    :   The cleaned up numeric input from the user
         *    
        */ 
        public double GetNumber(byte id)
        {
            // print prompt based on number id
            Console.WriteLine("Enter the " + ((id == 1) ? "1st" : "2nd" ) + " number, then press Enter.");
            
            string numInput = Console.ReadLine();

            double cleanNum;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput = Console.ReadLine();
            }

            return cleanNum;
        }

        // --- GetOperatorID --- // 
        /* 
         * Gets an operator input from the user while handling user input errors
         * 
         * Args: <void>
         *    
         * Returns: 
         *      opID<char>    :   The cleaned up operator identifier input from the user
         *    
        */
        public char GetOperatorID()
        {
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\te - Exponent");
            Console.Write("Your option? ");

            // array of valid operator inputs
            char[] validOper = { 'a', 's', 'm', 'd', 'e' };
            string input;  // string to store user inputs

            input = Console.ReadLine();

            char opID;      // char to store the operator id identified by the user

            while (!(char.TryParse(input, out opID) && validOper.Contains(opID)))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }

            return opID;
        }

        // --- GetOperatorID --- // 
        /* 
         * Calculates the result of the calculator process and stores it in the process object
         * 
         * Args: <void>
         *    
         * Returns: <void>
        */
        public void DoOperation() 
        {
            // update log file
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            // Use a switch statement to do the math.
            switch (operID)
            {
                case 'a':
                    result = num1 + num2;
                    oper = '+';
                    writer.WriteValue("Add");
                    break;
                case 's':
                    result = num1 + num2;
                    oper = '-';
                    writer.WriteValue("Subtract");
                    break;
                case 'm':
                    result = num1 + num2;
                    oper = 'x';
                    writer.WriteValue("Multiply");
                    break;
                case 'd':
                    // Ask the user to enter a non-zero divisor.
                    while (num2 == 0)
                    {
                        Console.WriteLine("Please enter a non-zero divisor!");
                        num2 = GetNumber(2);
                    }
                    result = num1 / num2;
                    oper = '/';
                    writer.WriteValue("Divide");
                    break;
                case 'e':
                    result = Math.Pow(num1, num2);
                    oper = '^';
                    writer.WriteValue("Exponent");
                    break;
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return;
         }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}