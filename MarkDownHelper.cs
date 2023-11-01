using Internet;
using Markdig;
using System.IO;

namespace Helper
{
    public class MarkDownHelper
    {
        public static bool OpenUserGuideFromLocal(string? name)
        {
            string MarkdownDoc = "";

            if (name is null)
                return false;

            //++ Read from embeded resource
            using (Stream? stream = File.OpenRead(name))
            {
                if (stream == null)
                {
                    MarkdownDoc = string.Format("Could not find sample text {0}", name);
                }
                else
                    using (StreamReader reader = new(stream))
                    {
                        MarkdownDoc = reader.ReadToEnd();
                    }
            }

            //++ Convert Markdown to Html
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseEmojiAndSmiley().UseTableOfContent().UsePipeTables().UseGridTables().Build();
            string markdownHtml = Markdown.ToHtml(MarkdownDoc, pipeline);
            string helpPath = $"{Directory.GetCurrentDirectory()}/UserGuide.html";
            File.WriteAllText(helpPath, markdownHtml);

            //++ Open with default Browser
            InternetHelper.OpenUrl(helpPath);

            /* 
            //++ Open with Webrowser
            //UserGuideContent.NavigateToString(markdownHtml); 
            Uri uri = new Uri(helpPath);
            UserGuideContent.Navigate(uri);
            UserManualView userManualView = WindowServices.OpenChildWindow(typeof(UserManualView), "MotorTest", window, this);
            */

            return true;
        }
    }
}
