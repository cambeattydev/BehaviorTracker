using System;
using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        
        internal DbSet<BehaviorTrackerUserRole> BehaviorTrackerUserRoles { get; set; }
        
        internal DbSet<BehaviorTrackerRole> BehaviorTrackerRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BehaviorTrackerRole>().HasData(SeedData.ListOfRoles);
            modelBuilder.Entity<BehaviorTrackerUser>().HasData(SeedData.ListOfUsers);
            modelBuilder.Entity<BehaviorTrackerUserRole>().HasData(SeedData.ListOfBehaviorTrackerUserRole);
            
#if DEBUG
            modelBuilder.Entity<Student>().HasData(SeedData.Testing.ListOfStudents);
            modelBuilder.Entity<Goal>().HasData(SeedData.Testing.ListOfGoals);
            modelBuilder.Entity<GoalAvailableAnswer>().HasData(SeedData.Testing.ListOfAvailableAnswers);
            //Creates goalAnswer test data for the time you are testing
            var currentHour = DateTime.Now.Hour > 15 ? 15 : DateTime.Now.Hour < 8 ? 8 : DateTime.Now.Hour;
            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentHour,
                currentHour >= 15 || currentHour == 8 ? 30 : DateTime.Now.Minute >= 30 ? 30 : 0, 0);
            modelBuilder.Entity<GoalAnswer>().HasData(SeedData.Testing.ListOfGoalAnswers(date));
            
            modelBuilder.Entity<BehaviorTrackerUser>().HasData(SeedData.Testing.ListOfUsers);
            modelBuilder.Entity<BehaviorTrackerUserRole>().HasData(SeedData.Testing.ListOfBehaviorTrackerUserRole);
            
#endif
        }


      
    }
}