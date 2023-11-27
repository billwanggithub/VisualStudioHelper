using Serilog;
using System;
using System.Windows.Controls;

namespace Helper
{
    public class GuiHelper
    {
        public static void InvokeOnUIThread(Control control, Action action)
        {
            if (control == null)
                return;

            try
            {
                if (control.Dispatcher.CheckAccess())
                {
                    control.Dispatcher.Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (Exception ex)
            {
                // log with exception here
                Log.Error("===========exception error================");
                Log.Error(ex.ToString());
                Log.Error("==========================================");
            }
        }
    }
}
