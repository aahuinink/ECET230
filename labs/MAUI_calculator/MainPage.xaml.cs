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
    string inputString = string.Empty;         // store the user's input string
    double outputNum;           // the number to be output to the user
    string outputString = string.Empty;        // the string to be output to the user

    public MainPage()
    {
        OutputState = OutputStates.Start;   // initialize at the starting state
        InitializeComponent();
    }

    private void InputChar(object sender, EventArgs e)
    {
        if(inputString == "0")              // delete leading zero and set state to 0
        {
            inputString = "";
        }
        Button btn = sender as Button;      // create a button object to contain the sender info
        var text = btn.Text;                // store the button text
        inputString += text;                // append the button text to the output string
        CalcOut.Text = inputString;
        OutputState = OutputStates.Input;
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

