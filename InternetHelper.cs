using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Internet
{
    public class InternetHelper
    {
        public static readonly string testUrl = "http://www.google.com"; // Replace with the URL you want to check
        public static readonly List<string> pingUrls = new() { @"www.google.com", @"gitee.com" }; // Replace with the URL you want to check

        public static async Task<bool> TestUrlAsync(string? url = null, int timeout = 1000)
        {
            // https://learn.microsoft.com/zh-tw/dotnet/api/system.threading.tasks.task.whenall?view=net-7.0
            url ??= testUrl;
            using HttpClient client = new();
            try
            {
                client.Timeout = TimeSpan.FromMilliseconds(timeout);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static async Task<string?> PingUrlAsync(List<string>? urls = null, int timeout = 2000)
        {
            urls ??= pingUrls;
            foreach (string url in urls)
            {
                var png = new Ping();
                try
                {
                    var reply = await png.SendPingAsync(url);
                    if (!(reply.Status == IPStatus.Success))
                    {
                        throw new TimeoutException("Unable to reach " + url + ".");
                    }
                    else
                    {
                        return url;
                    }
                }
                catch (PingException)
                {

                }
                //var tasks = new List<Task>();
                //tasks.Add(Task.Run(() =>
                //{
                //    var png = new Ping();
                //    try
                //    {
                //        var reply = png.Send(url);
                //        if (!(reply.Status == IPStatus.Success))
                //        {
                //            throw new TimeoutException("Unable to reach " + url + ".");
                //        }
                //    }
                //    catch (PingException)
                //    {
                //        throw;
                //    }
                //}));

                //try
                //{
                //    await Task.WhenAll(tasks);
                //    return true;
                //}
                //catch (Exception e)
                //{
                //    return false;
                //}
            }
            return null;


            //Task t = Task.WhenAll(tasks);

            //try
            //{
            //    //t.Wait();
            //    await t.ConfigureAwait(false);
            //}
            //catch { }

            //if (t.Status == TaskStatus.RanToCompletion)
            //{
            //    return true;
            //}
            //else //if (t.Status == TaskStatus.Faulted)
            //{
            //    return false;
            //}
        }

        public static void OpenUrl(string url)
        {
            // https://stackoverflow.com/questions/502199/how-to-open-a-web-page-from-my-application
            // For .NET Core, the default for ProcessStartInfo.UseShellExecute has changed from true to false, and so you have to explicitly set it to true for this to work;
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
    }
}
