using Microsoft.EntityFrameworkCore;

namespace VKR.src.Database
{
    internal class ErrorContext : DbContext
    {
        public DbSet<Error> Errors => Set<Error>();
        public ErrorContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=errors.db");
        }
    }
}
