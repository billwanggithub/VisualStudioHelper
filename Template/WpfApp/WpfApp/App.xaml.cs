using Helper;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex? singleInstanceMutex;
        public new static App Current => (App)Application.Current;
        public IServiceProvider? Services { get; }

        //private Mutex? singleInstanceMutex;

        public ViewModel? viewModel;  //用APP.Current.ViewMode存取, 如果找不到物件時，會直接回傳 null 空值
        public static MainWindow? mainWindow { get; set; }

        public App()
        {
            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ViewModel>(); // 整個 Process 只建立一個 Instance，任何時候都共用它, 要整個 Process 共用一份的服務可註冊成 Singleton
            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            InitLogFile("logs//Log.txt");

            // Get window title name
            //MainWindow window = new MainWindow();
            //string mutexName = window.Title;// "WpfApp";
            //window.Close();
            string windowTitle = ViewModel.windowTitle;

            /* 避免重複開視窗 */
            // Attempt to create the mutex
            singleInstanceMutex = new Mutex(true, windowTitle, out bool createdNew);

            // If the mutex is already created by another instance, exit the application
            if (!createdNew)
            {
                string msg = "Another instance of the application is already running.";
                Log.Warning(msg);
                MessageBox.Show(msg);
                WindowsService.ActivateWindow(windowTitle);
                Application.Current.Shutdown();
                Process.GetCurrentProcess().Kill();
            }

            Log.Warning("Create New Window");
            mainWindow = new MainWindow(); // 先定義window, 後定義viewmodel, 才能順利存取viewmodel內的window instance
            viewModel = Services?.GetService<ViewModel>(); // Get ViewModel
            mainWindow.DataContext = viewModel;
            viewModel!.mainWindow = mainWindow;

            // Show MainWIndow
            this.MainWindow = mainWindow;
            this.MainWindow!.Top = 0;
            this.MainWindow.Left = 0;
            this.MainWindow.Show();

            SetupExceptionHandling();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //MessageBox.Show("App Exist");
            // Release the mutex on application exit
            singleInstanceMutex?.ReleaseMutex();
            base.OnExit(e);
        }

        /* Reference:
         * https://stackoverflow.com/questions/793100/globally-catch-exceptions-in-a-wpf-application
        */
        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            string message = $"Unhandled exception ({source})";
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception in LogUnhandledException");
            }
            finally
            {
                Log.Error(exception, message);
            }
        }

        public static void InitLogFile(string logPath)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(logPath,//檔保存路徑                
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",//輸出日期格式
            //buffered: true,
            shared: true,
            rollingInterval: RollingInterval.Month,//日誌按月保存
            rollOnFileSizeLimit: false,          // 限制單個檔的最大長度   
            encoding: Encoding.UTF8,            // 檔字元編碼     
            retainedFileCountLimit: null,         // 最大保存文件數     
            fileSizeLimitBytes: null)      // 最大單個文件長度
            .WriteTo.Console()
            .CreateLogger();
        }

    }
}
