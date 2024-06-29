using Microsoft.EntityFrameworkCore;

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
