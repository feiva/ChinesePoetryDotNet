using ChinesePoetry.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Concurrent;

namespace ChinesePoetry.Console
{
    using ChinesePoetry.Database.Models;
    using EFCore.BulkExtensions;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static object sync_lock = new object();
        static void Main(string[] args)
        {
            //using (var db = new PoetryDbContext())
            //{
            //    // AddCi(db);
            //}
            var length = 100;
            var list = new long[length];
            var watch = new Stopwatch();
            watch.Start();
            var result1 = list.Select(i => { Thread.Sleep(100); return i; });
            watch.Stop();

            var time1 = watch.ElapsedMilliseconds;

            watch.Reset();
            watch.Restart();
            var result2 = list.AsParallel().Select(i => { Thread.Sleep(100); return i; });
            watch.Stop();

            var time2 = watch.ElapsedMilliseconds;

            Console.WriteLine($"time1:{time1}   time2:{time2}");
            Console.ReadKey();
        }

        private static void AddTangShi(PoetryDbContext context)
        {
            var path = @"F:\code\ChinesePoetry\ChinesePoetry.Console\data\json";
            var files = Directory.GetFiles(path, "*.json");
            var authorFiles = files.Where(f => f.Contains("author"));
            var shiFiles = files.Where(f => f.Contains("poet"));

        }

        //private static void AddCi(PoetryDbContext context)
        //{
        //    var path = @"F:\code\ChinesePoetry\ChinesePoetry.Console\data\ci";
        //    var files = Directory.GetFiles(path, "*.json");
        //    var authorFiles = files.Where(f => f.Contains("author"));
        //    var ciFiles = files.Where(f => f.Contains("ci.song"));

        //    if (authorFiles != null)
        //    {
        //        context.Authors.RemoveRange(context.Authors.Where(a => true));
        //        context.SaveChanges();
        //        List<Author> authors = new List<Author>(100000);
        //        var result = Parallel.ForEach(authorFiles, (file, loopState) =>
        //        {
        //            using (var reader = File.OpenText(file))
        //            {
        //                var json = reader.ReadToEnd();
        //                lock (sync_lock)
        //                {
        //                    authors.AddRange(JsonConvert.DeserializeObject<List<CiAuthor>>(json).Select(a => a.ToEntity()));
        //                }
        //            }

        //        });
        //        if (result.IsCompleted)
        //        {
        //            context.Authors.AddRange(authors);
        //            context.SaveChanges();
        //            Console.WriteLine("Ok");
        //        }
        //    }
        //    if (ciFiles != null)
        //    {
        //        context.Poetry.RemoveRange(context.Poetry.Where(a => true));
        //        context.SaveChanges();
        //        List<Poetry> cis = new List<Poetry>(100000);
        //        var result = Parallel.ForEach(ciFiles, (file, loopState) =>
        //            {
        //                var dynasty = file.Split(".", StringSplitOptions.RemoveEmptyEntries)[1];
        //                using (var reader = File.OpenText(file))
        //                {
        //                    var json = reader.ReadToEnd();
        //                    lock (sync_lock)
        //                    {
        //                        cis.AddRange(JsonConvert.DeserializeObject<List<CiContent>>(json).Select(c => c.ToEntity(dynasty)));
        //                    }
        //                }

        //            });
        //        cis = cis.Where(c => c != null && !string.IsNullOrEmpty(c.Title) && !string.IsNullOrEmpty(c.Content)).ToList();

        //        if (result.IsCompleted && cis != null)
        //        {
        //            // 20644
        //            Console.WriteLine(cis?.Count);
        //            context.AddRange(cis);
        //            context.SaveChanges();
        //            Console.WriteLine("Ok");
        //        }
        //    }
        //}
    }


    public class CiAuthor
    {
        public string description { get; set; }
        public string name { get; set; }
        public string short_description { get; set; }

        public Author ToEntity() => new Author
        {
            Description = description,
            Name = name
        };
    }

    public class CiContent
    {
        /// <summary>
        /// 和岘
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> paragraphs { get; set; }
        /// <summary>
        /// 导引
        /// </summary>
        public string rhythmic { get; set; }

        public Poetry ToEntity(string dynasty = null) => new Poetry
        {
            Title = rhythmic,
            Content = string.Join('\n', paragraphs),
            AuthorName = author,
            Type = PoetryType.Ci,
            Dynasty = dynasty
        };
    }
}
