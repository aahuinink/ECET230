namespace MAUI_calculator;


public partial class MainPage : ContentPage
{
    // create an enum to handle the changes in output states
    enum StateInput
    {
        Start,  // everything at 0 (clear pressed more than once)
        Root,   // the first number entered after being in start state
        Child,  // any numbers entered after root number
        Display // displaying the final output
    }

    enum Operator // enum to store the last operator button pressed
    {
        None,     
        Plus,
        Minus,
        Times,
        Divide
    }

    // variables to manage things
    StateInput InputState;   // handle output states
    double previousNum;         // store the number previously inputted
    double currentNum;          // store the number the user just inputted
    string inputString = string.Empty;         // store the user's input string
    double outputNum;           // the number to be output to the user
    string outputString = string.Empty;        // the string to be output to the user

    public MainPage()
    {
        InputState= StateInput.Start;   // initialize at the starting state
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
        InputState= StateInput.Input;
        return;
    }

    private void ResetValues()
    {
        InputState= StateInput.Start;   // set back to start
        currentNum = 0;                     // and reset all values
        previousNum = 0;
        outputString = "0";
        inputString = "0";
    }

    private void buttonClear_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonOperator_Clicked()
    {

    }
}

