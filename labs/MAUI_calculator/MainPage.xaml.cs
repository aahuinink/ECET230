using System.Security.Cryptography.X509Certificates;
using MAUI_calculator.ViewModel;

namespace MAUI_calculator;


public partial class MainPage : ContentPage
{
    // create an enum to handle the changes in output states
    enum OutputStates
    {
        Start,
        Input,
        Display
    }

    // variables to manage things
    OutputStates OutputState;   // handle output states
    double previousNum;         // store the number previously inputted
    double currentNum;          // store the number the user just inputted
    string inputString;         // store the user's input string
    double outputNum;           // the number to be output to the user
    string outputString;        // the string to be output to the user

    public MainPage()
    {
        OutputState = OutputStates.Start;   // initialize at the starting state
        outputString = "0";                 // initializ output string to 0
        inputString = "0";                   // initialize input string

        CalcOut.Text = inputString;        // display the initial output string
        InitializeComponent();
    }

    private void InputChar(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        var text = btn.Text;
        inputString += text;
        CalcOut.Text = inputString;
        return;
    }

    private void ResetValues()
    {
        OutputState = OutputStates.Start;   // set back to start
        currentNum = 0;                     // and reset all values
        previousNum = 0;
        outputString = "0";
        inputString = "0";
    }

    private void buttonClear_Clicked(object sender, EventArgs e)
    {
        switch (OutputState)
        {
            case OutputStates.Start:
                break;
            case OutputStates.Input:
                if (inputString == "0")     // if the user hasn't input any data and presses clear
                {
                    ResetValues();          // reset all values
                }
                else
                {
                    inputString = "0";      // otherwise just reset the input string, but keep all other values
                }
                break;
            case OutputStates.Display:
                ResetValues();
                break;
            default:
                break;
        }
        CalcOut.Text = inputString;         // display  the input string
    }

    private void button2nd_Clicked(object sender, EventArgs e)
    {

    }


    private void buttonPlus_Clicked(object sender, EventArgs e)
    {
        if(double.TryParse(inputString, out currentNum))
        {
            
        }
    }

    private void buttonMinus_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonTimes_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonEquals_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonDivide_Clicked(object sender, EventArgs e)
    {

    }
}

