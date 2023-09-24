using System.Xml;

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
    string inputString;
    string resultString;
    string oper;

    public MainPage()
    {
        InputState= StatesOfInput.Start;    // initialize at the starting state
        inputString = "";                  // initialize all strings to empty and output to 0
        resultString = "";
        oper = "";
        CalcOut.Text = "0";
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

    // --- CharButton_clicked --- //
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

    private string DoOperation()
    {
        string stringResult = string.Empty;  // stores the string result of the operation

        return stringResult;
    }

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

    private void EnterButton_clicked(object obj, EventArgs e)
    {
        Button button = (Button)obj;
        switch (InputState)
        {
            case StatesOfInput.Child:
                if (string.IsNullOrEmpty(inputString))
                {

                }
        }
    }
}

