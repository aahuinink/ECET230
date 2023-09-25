

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MAUI_calculator;


public partial class MainPage : ContentPage
{
    // create an enum to handle the changes in output states
    enum StatesOfInput
    {
        Start,      // everything at 0 (clear pressed more than once)
        Root,       // the first number entered after being in start state
        Child,      // any numbers entered after root number
        Display,    // displaying the final output
        AwaitClear, // awaiting the 2nd clear press
    }

    // variables to manage things
    StatesOfInput InputState;   // handle output states
    string inputString;         // store input
    string resultString;        // store result
    string oper;                // store operator
    bool secondFlag = false;    // store if 2nd button has been clicked

    public MainPage()
    {
        InputState= StatesOfInput.Start;    // initialize at the starting state
        InitializeComponent();
    }

    // --- CharButton_clicked --- //
    // Handles a character button click (0-9, '.')
    private void CharButton_clicked(object sender, EventArgs e)    
    {
        Button btn = (Button)sender;                // create a button object to store the sender info
        switch (InputState)                         // control switch case
        {
            case StatesOfInput.Start:               // if start
                inputString = btn.Text;             // set output to the char passed by the button
                oper = "";                          // reset operator
                InputState = StatesOfInput.Root;    // set state to root
                break;
            case StatesOfInput.Root:                // if first number
                inputString += btn.Text;            // append the button text to the input string
                break;
            case StatesOfInput.Child:
                inputString += btn.Text;            // same as root...
                break;
            case StatesOfInput.Display:             // if displaying
                resultString = "";                  // clear the result string
                inputString = btn.Text;             // set input to the button text
                oper = "";                          // reset operator
                InputState = StatesOfInput.Root;    // set state to root
                break;
            case StatesOfInput.AwaitClear:          // if awaiting 2nd clear press
                inputString = btn.Text;             // set input string to button text
                InputState = StatesOfInput.Child;   // set state back to child
                break;
            default:
                break;
        }
        CalcOut.Text = inputString;                 // output to user
    }

    // --- OperButton_clicked --- //
    // Handles an operator button clicked
    private void OperButton_cliked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        switch (InputState)
        {
            case StatesOfInput.Start:                   // if start do nothing
                break;
            case StatesOfInput.Root:                    // store input, reset input, save operator button
                resultString = inputString;
                inputString = "";
                InputState = StatesOfInput.Child;       // change to child state
                oper = btn.Text; 
                break;
            case StatesOfInput.Child:                   // do operation, store result, clear input
                resultString = DoOperation();
                inputString = "";
                oper = btn.Text;                    
                CalcOut.Text = resultString;            // output to user
                break;
            case StatesOfInput.Display:                 // store operator, reset input, revert to child state
                oper = btn.Text;
                inputString = "";
                InputState = StatesOfInput.Child;
                break;
            case StatesOfInput.AwaitClear:              
                oper = btn.Text;
                inputString = "";
                CalcOut.Text = "0";
                break;
            default:
                break;
        }
    }

    // --- ClearButton_clicked --- //
    // Handles the clear button action
    private void ClearButton_clicked(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        switch (InputState) {
            case StatesOfInput.Start:
                break;
            case StatesOfInput.Root:
                resultString = "";
                inputString = "";
                CalcOut.Text = "0";
                InputState = StatesOfInput.Start;
                break;
            case StatesOfInput.Child:
                InputState = StatesOfInput.AwaitClear;
                break;
            case StatesOfInput.Display:
                resultString = "";
                inputString = "";
                CalcOut.Text = "0";
                InputState = StatesOfInput.Start;
                break;
            case StatesOfInput.AwaitClear:
                resultString = "";
                inputString = "";
                CalcOut.Text = "0";
                InputState = StatesOfInput.Start;
                break;
            default:
                break;
        }
    }

    // --- EnterButton_clicked --- //
    // Handles the enter button action
    private void EnterButton_clicked(object obj, EventArgs e)
    {
        Button button = (Button)obj;
        switch (InputState)
        {
            case StatesOfInput.Child:                       // if there's actually something to do
                if (string.IsNullOrEmpty(inputString))      // and there's input as well
                {
                    break;
                }
                else
                {
                    resultString = DoOperation();           // do the operation, reset the input and operator strings, and display the result
                    inputString = "";
                    oper = "";
                    CalcOut.Text = resultString;
                    InputState = StatesOfInput.Display;     // change to display state
                    break;

                }
            default:    // otherwise, do nothing
                break;  
        }
    }

    // --- SecondButton_clicked --- //
    // handles 2nd button click
    private void SecondButton_clicked(object sender, EventArgs e)
    {
        if (secondFlag)
        {
            buttonPlus.Text = "^";
        } else
        {
            buttonPlus.Text = "+";
        }
        secondFlag = secondFlag ^ true; // toggle secondFlag
    }
    

    // --- DoOperation --- // 
    // does the math operation requested and handles errors...
    private string DoOperation()
    {
        string result = string.Empty;  // stores the string result of the operation
        double prevNum;     // the previously entered number
        double currentNum;  // the current input number

        if(!double.TryParse(resultString, out prevNum)) {   // if the first entry can't be parsed
            CalcOut.Text = "INVALID ENTRY";             // print out warning
            inputString = "";                           // reset all variables
            resultString = "";
            oper = "";
            InputState = StatesOfInput.Start;           // reset to starting state
        }

        if(!double.TryParse(inputString, out currentNum))   // if the second entry can't be parsed
        {   
            CalcOut.Text = "INVALID ENTRY";             // print out warning
            inputString = "";                           // reset all variables
            resultString = "";
            oper = "";
            InputState = StatesOfInput.Start;           // reset to starting state
        }

        switch (oper)
        {
            case "+":
                // RICK-ROLL EASTER EGG
                if (prevNum == 2 & currentNum == 2) // ask stupid questions...
                {
                    OpenBrowser("https://www.youtube.com/watch?v=dQw4w9WgXcQ");  // get stupid answers :)
                    CalcOut.Text = ":)";                // reset everything and send back to start
                    inputString = "";
                    resultString = "";
                    oper = "";
                    InputState = StatesOfInput.Start;
                    break;
                } else {
                    result = (prevNum + currentNum).ToString();         // return sum of the 2 numbers
                    break;
                }
            case "-":
                result = (prevNum - currentNum).ToString(); break;  // return difference of the two numbers
            case "x":
                result = (prevNum * currentNum).ToString(); break;  // return product
            case "/":
                if ((prevNum / currentNum) == double.NaN)           // if divide by zero
                {
                    CalcOut.Text = "DIV BY 0 ERROR";                // print out warning to user
                    inputString = "";                           // reset all variables
                    resultString = "";
                    oper = "";
                    InputState = StatesOfInput.Start;           // reset to starting state
                } else
                {
                    result = (prevNum / currentNum).ToString();     // otherwise return fraction
                }
                break;
            case "^":
                result = (Math.Pow(prevNum, currentNum)).ToString(); break; // return power
            default:
                CalcOut.Text = "INVALID";
                inputString = "";
                resultString = "";
                oper = "";
                InputState = StatesOfInput.Start; break;
        }

        return result;
    }

    // from Brock Allen for help with opening url :) https://stackoverflow.com/questions/14982746/open-a-browser-with-a-specific-url-by-console-application 
    public static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}

