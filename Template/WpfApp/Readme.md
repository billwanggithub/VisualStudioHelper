# Template for Wpf MVVM project

- Install MVVM package `CommunityToolkit.Mvvm`
- Change the window title in `ViewModel.cs`

```csharp=
[ObservableProperty]
public static string windowTitle = "WpfApp";
```

## Reference

 - [Dependency Injection概念介紹](https://ithelp.ithome.com.tw/articles/10204404)
 - [不可不知的 ASP.NET Core 依賴注入](https://blog.darkthread.net/blog/aspnet-core-di-notes/)
 - [如何在 .NET Core 主控台專案中使用 DI (相依注入) 並取得 ILogger 服務](https://blog.miniasp.com/post/2019/01/05/How-to-use-DI-and-ILogger-in-Console-program)
 - [如何使用Serilog 错误日志](https://blog.csdn.net/weixin_39499738/article/details/116048673)
 - [Handling exceptions in WPF](https://wpf-tutorial.com/wpf-application/handling-exceptions/)
