
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI_calculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        int currentNum;

        [ObservableProperty]
        string currentNumString;

        [RelayCommand]
        void Add()
        {
            // add something
            CurrentNum++;
            CurrentNumString = CurrentNum.ToString();
            return;
        }
    }
}
