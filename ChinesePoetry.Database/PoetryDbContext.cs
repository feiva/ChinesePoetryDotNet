using ChinesePoetry.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChinesePoetry.Database
{
    public class PoetryDbContext : DbContext
    {
        //public PoetryDbContext(DbContextOptions<PoetryDbContext> options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Data Source=localhost;Database=chinesepoetry;User ID=root;Password=P@ssw0rd1234;pooling=true;CharSet=utf8;port=3306;sslmode=none");
        }

        public DbSet<Poetry> Poetry { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
