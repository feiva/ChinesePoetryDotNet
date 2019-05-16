using ChinesePoetry.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ChinesePoetry.Database
{
    public class PoetryDbContext
    {
        private readonly IConfiguration _config;
        private readonly IMongoDatabase _database;
        public PoetryDbContext(IConfiguration config)
        {
            _config = config;

            var connection = _config["MongoDbConnectionString"];
            var client = new MongoClient(connection);
            if (client != null)
            {
                _database = client.GetDatabase("chinesepoetry");
            }
            else
            {
                throw new Exception("cannot connect to mongodb!");
            }
        }

        public IMongoCollection<MingJu> MingJu
        {
            get
            {
                var list = _database.ListCollections()
                    .ToList().
                    Select(a => a["name"].AsString);
                if (!list.Any(a => a.Equals(nameof(MingJu), StringComparison.CurrentCultureIgnoreCase)))
                {
                    _database.CreateCollection(nameof(MingJu));
                }
                return _database.GetCollection<MingJu>(nameof(MingJu));
            }
        }
    }
}
