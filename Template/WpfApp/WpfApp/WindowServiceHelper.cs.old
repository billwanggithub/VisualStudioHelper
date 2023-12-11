using Serilog;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Interop;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;

// https://www.pinvoke.net/index.aspx

namespace Helper
{
    public class WindowsService
    {
        //[DllImport("user32.dll")]
        //public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        //[DllImport("user32.dll")]
        //public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        //[DllImport("user32.dll", SetLastError = true)]
        //static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32")]
        //public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        //public const uint MF_GRAYED = 0x00000001;
        //public const uint MF_ENABLED = 0x00000000;
        public const uint SC_CLOSE = 0xF060;

        /// <summary>
        /// Enable/Disable Window Close Button
        /// </summary>
        /// <param name="window"></param>
        /// <param name="enable"></param>
        public static void EnableCloseButton(Window window, bool enable)
        {
            window.Dispatcher.Invoke(new Action(() =>
            {
                var hwnd = new WindowInteropHelper(window).Handle;
                var hMenu = GetSystemMenu(hwnd, false);
                if (enable)
                    EnableMenuItem(hMenu, SC_CLOSE, MenuFlags.MF_ENABLED);
                else
                    EnableMenuItem(hMenu, SC_CLOSE, MenuFlags.MF_GRAYED);
            }));
        }




        public static HWND ActivateWindow(string windowname)
        {
            HWND otherWindow = FindWindow(null, windowname);
            Log.Information($"Find Window = {otherWindow}");
            // Set the window to be topmost
            SetWindowPos(otherWindow, HWND.HWND_TOPMOST, 0, 0, 800, 800, SetWindowPosFlags.SWP_SHOWWINDOW);
            SetForegroundWindow(otherWindow);
            ShowWindow(otherWindow, ShowWindowCommand.SW_RESTORE);
            //if (otherWindow != IntPtr.Zero) // Already Running
            //{
            //    SetWindowPos(otherWindownew, IntPtr(-1), 0, 0, 0, 0, TOPMOST_FLAGS | SWP_SHOWWINDOW);
            //    childWindow!.Topmost = false;
            //    childWindow!.WindowState = WindowState.Normal;
            //    childWindow!.Activate();
            //    SetForegroundWindow(otherWindow);
            //}
            return otherWindow;
        }

        /// <summary>
        /// Open a Child WIndow
        /// </summary>
        /// <param name="childWindowType">Use typeof(child_window_name)</param>
        /// <param name="tag"></param>
        /// <param name="parentWindow"></param>
        /// <param name="dataContext">ViewVodel for the child window</param>
        /// <returns></returns>
        public static dynamic? OpenChildWindow(Type childWindowType, string? tag = "", Window? parentWindow = null, object? dataContext = null)
        {
            Window? childWindow = null;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                WindowCollection? windows = Application.Current.Windows;
                // Filter the windows to only include child windows
                childWindow = windows.OfType<Window>().FirstOrDefault(w => w.GetType() == childWindowType);
                if (childWindow == null)
                {
                    // Show the child window
                    //parentWindow!.Dispatcher.Invoke(() =>
                    //{
                    // Create an instance of the child window using the specified type
                    childWindow = Activator.CreateInstance(childWindowType) as Window;

                    // Set the parent window of the child window
                    if (parentWindow != null)
                    {
                        // Set the position of the child window relative to the parent window
                        childWindow!.Left = parentWindow.Left + 20;
                        childWindow.Top = parentWindow.Top + 20;
                        // Set Owner
                        //childWindow!.Owner = Application.Current.MainWindow; // Child window will always on top of parent window
                        childWindow!.Owner = parentWindow; // Child window will always on top of parent window
                                                           //childWindow.Owner = null;
                    }

                    childWindow!.Title = tag;
                    childWindow.Tag = tag;
                    childWindow.DataContext = dataContext;
                    childWindow.Show();
                    childWindow.Topmost = false;
                    //});
                }
                else
                {
                    childWindow.Owner = parentWindow;
                    childWindow.Topmost = false;
                    childWindow.WindowState = WindowState.Normal;
                    childWindow.Activate();
                }
            });
            return childWindow;
        }
    }
}
