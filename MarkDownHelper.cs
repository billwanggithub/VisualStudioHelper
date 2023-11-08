using Internet;
using Markdig;
using Markdig.Syntax;

namespace Helper
{
    public class MarkDownHelper
    {
        /// <summary>
        /// Convert MarkDown to HTML
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool OpenUserGuideFromLocal(string? input, string? output = null)
        {
            string MarkdownDoc = "";

            if (input is null)
                return false;

            //++ Read from embeded resource
            using (Stream? stream = File.OpenRead($"{input}"))
            {
                if (stream == null)
                {
                    MarkdownDoc = string.Format("Could not find sample text {0}", input);
                }
                else
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        MarkdownDoc = reader.ReadToEnd();
                    }
            }

            //++ Convert Markdown to Html
            var pipeline = new MarkdownPipelineBuilder()
                .UseAbbreviations()
                .UseAutoIdentifiers()
                .UseCitations()
                .UseDefinitionLists()
                .UseEmphasisExtras()
                .UseFigures()
                .UseFooters()
                .UseFootnotes()
                .UseGridTables()
                .UseMathematics()
                .UseMediaLinks()
                .UsePipeTables()
                .UseListExtras()
                .UseTaskLists()
                .UseDiagrams()
                .UseAutoLinks()
                .UseGenericAttributes()
                .UseEmojiAndSmiley()
                .UseTableOfContent()
                .UsePipeTables()
                .UseGridTables()
                .UseMathematics()
                .UseTableOfContent()
                .Build();

            MarkdownDocument document = Markdown.Parse(MarkdownDoc, pipeline);
            string markdownHtml = document.ToHtml(pipeline);


            string helpPath = $"{Directory.GetCurrentDirectory()}/{Path.GetFileNameWithoutExtension(input)}.html";
            if (output is not null)
            {
                helpPath = $"{Directory.GetCurrentDirectory()}/{Path.GetFileNameWithoutExtension(output)}.html";
            }

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
