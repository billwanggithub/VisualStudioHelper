using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows;


// Please Install Vanara.PInvoke.Kernel32, Vanara.PInvoke.Kernel32, Vanara.PInvoke.User32
// https://www.pinvoke.net/index.aspx

namespace Helper
{
    public class WindowsService
    {

        public static string GetSystemInfo()
        {
            List<string> lines = new List<string>();
            string return_string = "";

            /* https://www.codeguru.com/columns/dotnet/using-c-to-find-out-what-your-computer-is-made-of.html
               https://ourcodeworld.com/articles/read/294/how-to-retrieve-basic-and-advanced-hardware-and-software-information-gpu-hard-drive-processor-os-printers-in-winforms-with-c-sharp
             */
            lines.Add("\n---------------- System Information ----------------");
            lines.Add($"Windows version: {Environment.OSVersion}");
            lines.Add($"64 Bit operating system ? : {(Environment.Is64BitOperatingSystem ? "Yes" : "No")}");

            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                lines.Add("Caption  -  " + obj["Caption"]);
                lines.Add("WindowsDirectory  -  " + obj["WindowsDirectory"]);
                lines.Add("ProductType  -  " + obj["ProductType"]);
                lines.Add("SerialNumber  -  " + obj["SerialNumber"]);
                lines.Add("SystemDirectory  -  " + obj["SystemDirectory"]);
                lines.Add("CountryCode  -  " + obj["CountryCode"]);
                lines.Add("CurrentTimeZone  -  " + obj["CurrentTimeZone"]);
                lines.Add("EncryptionLevel  -  " + obj["EncryptionLevel"]);
                lines.Add("OSType  -  " + obj["OSType"]);
                lines.Add("Version  -  " + obj["Version"] + "\n");
            }

            ManagementClass myManagementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection myManagementCollection =
               myManagementClass.GetInstances();
            PropertyDataCollection myProperties =
               myManagementClass.Properties;
            Dictionary<string, object> myPropertyResults =
               new Dictionary<string, object>();

            foreach (var obj in myManagementCollection)
            {
                foreach (var myProperty in myProperties)
                {
                    myPropertyResults.Add(myProperty.Name,
                       obj.Properties[myProperty.Name].Value);
                }
            }

            foreach (var myPropertyResult in myPropertyResults)
            {
                lines.Add($"{myPropertyResult.Key}: {myPropertyResult.Value}");
            }
            lines.Add("----------------------------------------------------");

            foreach (string line in lines)
            {
                return_string += line + "\n";
            }
            return return_string;
        }

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
            var otherWindow = FindWindow(null, windowname);
            Log.Information($"Find Window = {otherWindow}");
            if (otherWindow != IntPtr.Zero) // Already Running
            {
                SetForegroundWindow(otherWindow);
            }
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
        public static dynamic? OpenChildWindow(Type childWindowType, string tag, Window? parentWindow = null, object? dataContext = null)
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

                    childWindow!.Tag = tag;
                    childWindow!.DataContext = dataContext;
                    childWindow.Show();
                    childWindow!.Topmost = false;
                    //});
                }
                else
                {
                    childWindow!.Owner = parentWindow;
                    childWindow!.Topmost = false;
                    childWindow!.WindowState = WindowState.Normal;
                    childWindow!.Activate();
                }
            });
            return childWindow;
        }

        /* http://www.itpers.info/post/2012/02/12/%E5%A6%82%E4%BD%95%E7%94%A8C%E5%B0%87%E9%9B%BB%E8%85%A6%E8%A8%AD%E5%AE%9A%E7%82%BA%E4%BC%91%E7%9C%A0%E6%88%96%E7%9D%A1%E7%9C%A0.aspx
        *   設為休眠模式
                Application.SetSuspendState(PowerState.Hibernate, false, false);
            設為睡眠模式
                Application.SetSuspendState(PowerState.Suspend, false, false);
        */

        /* 避免系統進入休眠狀態
        * http://andy51002000.blogspot.com/2016/06/c.html.
        * http://www.aspphp.online/bianchen/dnet/cxiapu/cxpjc/201701/132725.html
        * https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate
        */

        [DllImport("kernel32.dll")]
        private static extern uint SetThreadExecutionState(uint esFlags);

        [FlagsAttribute]
        public enum EXECUTION_FLAG : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }

        /*
            實現下載時阻止程序休眠，則有兩種實現方式：
                --下載期間起計時器定期執行ResetSleepTimer函數
                --下載開始時執行PreventSleep函數，下載結束後執行ResotreSleep函數。
        */

        /// <summary>
        ///阻止系統休眠，直到線程結束恢復休眠策略
        /// </summary>
        /// <param name="includeDisplay">是否阻止關閉顯示器</param>
        public static void PreventSleep(bool includeDisplay = false)
        {
            //Enable S3\S4
            if (includeDisplay)
                SetThreadExecutionState((uint)(EXECUTION_FLAG.ES_SYSTEM_REQUIRED | EXECUTION_FLAG.ES_DISPLAY_REQUIRED | EXECUTION_FLAG.ES_CONTINUOUS));
            else
                SetThreadExecutionState((uint)(EXECUTION_FLAG.ES_SYSTEM_REQUIRED | EXECUTION_FLAG.ES_CONTINUOUS));
        }

        /// <summary>
        ///恢復系統休眠策略
        /// </summary>
        public static void ResotreSleep()
        {
            //Enable S3\S4
            SetThreadExecutionState((uint)ExecutionFlag.Continus);
        }
    }
}
