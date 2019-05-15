using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChinesePoetry.Spider
{



    class Program
    {
        static void Main(string[] args)
        {
            MingJuSpider.Run();
            Console.ReadKey();
        }

    }

    /// <summary>
    /// 名句
    /// </summary>
    public class MingJuSpider
    {
        static string MingJu = "https://so.gushiwen.org/mingju/default.aspx?p={0}&c={1}&t={2}";
        static int MingJuMaxPage = 20;
        static object syncLock = new object();

        public static void Run()
        {
            ConsoleHelper.Info("============================Start============================");

            var web = new HtmlAgilityPack.HtmlWeb();
            var mingJuList = new List<MingJu>();

            var result = Parallel.For(0, MingJuMaxPage, index =>
            {
                var taskId = $"Task#{index}";
                var url = string.Format(MingJu, index + 1, string.Empty, string.Empty);
                ConsoleHelper.Info($"Start: {taskId}");
                var page = web.LoadFromWebAsync(url).Result;
                var list = page.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/div[2]/div");
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        var content = item.SelectSingleNode("a[1]")?.InnerText;
                        var source = item.SelectSingleNode("a[2]")?.InnerText;
                        if (string.IsNullOrEmpty(content))
                        {
                            continue;
                        }
                        lock (syncLock)
                        {
                            mingJuList.Add(new Spider.MingJu(content, source));
                        }
                    }
                }
                ConsoleHelper.Success($"Success: {taskId}");
            });

            if (result.IsCompleted)
            {
                ConsoleHelper.Success($"Total: {mingJuList.Count}");
                ConsoleHelper.Info("=============================End=============================");

                foreach(var item in mingJuList)
                {
                    ConsoleHelper.Info($"{item.Content} —— {item.Source}");
                }
            }
        }
    }

    public class ConsoleHelper
    {
        static ConsoleColor CurrentColor = Console.ForegroundColor;

        public static void Info(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(info);
            Console.ForegroundColor = CurrentColor;
        }

        public static void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = CurrentColor;
        }

        public static void Error(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(error);
            Console.ForegroundColor = CurrentColor;
        }
    }

    public class MingJu
    {
        public MingJu(string content, string source)
        {
            Content = content;
            Source = source;
        }

        public string Content { get; set; }
        public string Source { get; set; }
    }
}
