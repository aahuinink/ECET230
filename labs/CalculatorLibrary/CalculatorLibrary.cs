using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public struct CalcOutput
    {
        public double num;
        public string oper;
    }

    public class Calculator
    {

        JsonWriter writer;

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        public CalcOutput DoOperation(double num1, double num2, string op)
        {
            CalcOutput result = new CalcOutput();
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result.num = num1 + num2;
                    result.oper = "+";
                    writer.WriteValue("Add");
                    break;
                case "s":
                    result.num = num1 - num2;
                    result.oper = "-";
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    result.num = num1 * num2;
                    result.oper = "x";
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    while (num2 == 0)
                    {
                        Console.WriteLine("Please enter a non-zero divisor!");
                        Console.WriteLine("Enter a divsor: ");
                        num2 = Convert.ToDouble(Console.ReadLine());
                    }
                    result.num = num1 / num2;
                    result.oper = "/";
                    writer.WriteValue("Divide");
                    break;
                case "e":
                    result.num = Math.Pow(num1, num2);
                    result.oper = "^";
                    writer.WriteValue("Exponent");
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result.num);
            writer.WriteEndObject();
            return result;
         }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}