using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository
{
    public class BehaviorTrackerDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BehaviorTracker.db");
        }
    }
}