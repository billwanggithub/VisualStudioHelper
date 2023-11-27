using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;


// TODO: Hotkey binding 
// TODO: Datagrid binding 

namespace WpfApp
{
    public partial class ViewModel : ObservableObject
    {
        [ObservableProperty]
        public static string windowTitle = "WpfApp";

        public MainWindow? mainWindow;

        #region Variable Binding
        [ObservableProperty]
        int intObj = 10;
        [ObservableProperty]
        string stringObj = "WpfApp";
        #endregion

        #region Command Binding
        [RelayCommand]
        void TestFunction(object? param)
        {
            int temp = 0;
            if (param != null)
            {
                temp = Convert.ToInt32(param);
            }
            IntObj += temp; // 110
            StringObj += " Test"; // WpfAPp Test
        }
        #endregion
    }
}
