using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;


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

        public class Person
        {
            public string? Name { get; set; }
            public int Age { get; set; }
        }

        [ObservableProperty]
        public ObservableCollection<Person>? people = new ObservableCollection<Person>
            {
                new Person { Name = "John Doe", Age = 30 },
                new Person { Name = "Jane Doe", Age = 25 },
                // Add more persons as needed
            };


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
