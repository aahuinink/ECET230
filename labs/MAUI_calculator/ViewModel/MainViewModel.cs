
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI_calculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {   
        enum StateInput
        {
            Start,      // user has pressed clear more than once, or the application has just begun
            Input,      // the user is inputting a new number
            Display     // the user has requested the final answer be displayed
        }

        //private variables
        //controls the output state
        StateInput OutputState;

        // variables to hold the current number the user is inputting
        [ObservableProperty]
        string currentNumString;

        [ObservableProperty]
        string previousNumString;

        [ObservableProperty]
        string outputString;

        // Command to add the current number to the previous number
        [RelayCommand]
        void SelectNumber()
        {
            
            return;
        }
    }
}
