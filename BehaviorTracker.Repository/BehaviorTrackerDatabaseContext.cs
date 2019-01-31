using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BehaviorTracker.Repository
{
    public class BehaviorTrackerDatabaseContext : DbContext
    {
        public static string ConnectionString = "Data Source=BehaviorTracker.db";
        internal DbSet<Goal> Goals { get; set; }
        internal DbSet<GoalAnswer> GoalAnswers { get; set; }
        internal DbSet<GoalAvailableAnswer> GoalAvailableAnswer { get; set; }
        internal DbSet<Student> Students { get; set; }

        internal DbSet<BehaviorTrackerUser> BehaviorTrackerUsers { get; set; }

        internal DbSet<BehaviorTrackerRoleGroup> BehaviorTrackerRoleGroups { get; set; }

        internal DbSet<BehaviorTrackerRoleGroupRole> BehaviorTrackerRoleGroupsRoles { get; set; }

        internal DbSet<BehaviorTrackerUserRoleGroup> BehaviorTrackerUserRoleGroups { get; set; }

        internal DbSet<BehaviorTrackerRole> BehaviorTrackerRoles { get; set; }

        internal DbSet<BehaviorTrackerUserManager> BehaviorTrackerUserManagers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BehaviorTrackerRole>().HasData(SeedData.Roles);
            modelBuilder.Entity<BehaviorTrackerUser>().HasData(SeedData.Users);
            modelBuilder.Entity<BehaviorTrackerRoleGroup>().HasData(SeedData.RoleGroups);
            modelBuilder.Entity<BehaviorTrackerRoleGroupRole>().HasData(SeedData.RoleGroupRoles);
            modelBuilder.Entity<BehaviorTrackerUserRoleGroup>().HasData(SeedData.UserRoleGroups);

            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);

//#if DEBUG
//            modelBuilder.Entity<Student>().HasData(SeedData.Testing.Students);
//            modelBuilder.Entity<Goal>().HasData(SeedData.Testing.Goals);
//            modelBuilder.Entity<GoalAvailableAnswer>().HasData(SeedData.Testing.AvailableAnswers);
//            //Creates goalAnswer test data for the time you are testing
//            var currentHour = DateTime.Now.Hour > 15 ? 15 : DateTime.Now.Hour < 8 ? 8 : DateTime.Now.Hour;
//            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentHour,
//                currentHour >= 15 || currentHour == 8 ? 30 : DateTime.Now.Minute >= 30 ? 30 : 0, 0);
//            modelBuilder.Entity<GoalAnswer>().HasData(SeedData.Testing.ListOfGoalAnswers(date));
//            modelBuilder.Entity<BehaviorTrackerUser>().HasData(SeedData.Testing.Users);
//            modelBuilder.Entity<BehaviorTrackerUserRoleGroup>().HasData(SeedData.Testing.UserRoleGroups);
//#endif
        }
    }
}