using System

public class Calculator {

	public void DoOperation(double num1, double num2, string op){
		// Use a switch statement to do the math.
		switch (op)
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
	}

}

