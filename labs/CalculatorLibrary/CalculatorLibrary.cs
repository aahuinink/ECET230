namespace CalculatorLibrary
{
    public struct CalcOutput
    {
        public double num;
        public string oper;
    }

    public class CalculatorFunctions
    {
        public static CalcOutput DoOperation(double num1, double num2, string op)
        {
            CalcOutput result = new CalcOutput(); // Default value is "not-a-number" if an operation, such as division, could result in an error.

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result.num = num1 + num2;
                    result.oper = "+";
                    break;
                case "s":
                    result.num = num1 - num2;
                    result.oper = "-";
                    break;
                case "m":
                    result.num = num1 * num2;
                    result.oper = "x";
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
                    break;
                case "e":
                    result.num = Math.Pow(num1, num2);
                    result.oper = "^";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }
            return result;
        }
    }
}