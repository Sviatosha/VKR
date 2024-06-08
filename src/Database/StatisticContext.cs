using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR.src.Database
{
    internal class StatisticContext : DbContext
    {

        public DbSet<Statistic> Statistics => Set<Statistic>();
        public StatisticContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=statistic.db");
        }
    }
}
