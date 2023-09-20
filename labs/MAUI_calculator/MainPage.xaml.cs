using System.Security.Cryptography.X509Certificates;
using MAUI_calculator.ViewModel;

namespace MAUI_calculator;


public partial class MainPage : ContentPage
{
    // Enum to store the state that the output is at
    public enum OutputStates
    {
        Root,
        Child
    }

    // create outputstate variable to store current state
    OutputStates OutputState;

    // create strings to store the numbers inputed by the user
    string previousNum; // the number previously entered by the user
    string currentNum; // the number currently being entered by the user

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void buttonOne_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonTwo_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonClear_Clicked(object sender, EventArgs e)
    {
        
    }

    private void button2nd_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonThree_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonPlus_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonFour_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonFive_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonSix_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonMinus_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonSeven_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonEight_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonNine_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonTimes_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonDecimal_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonZero_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonEquals_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonDivide_Clicked(object sender, EventArgs e)
    {

    }
}

