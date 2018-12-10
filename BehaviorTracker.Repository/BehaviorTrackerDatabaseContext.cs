using BehaviorTracker.Repository.Models;
using BehaviorTracker.Shared;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(ListOfStudents);
            modelBuilder.Entity<Goal>().HasData(ListOfGoals);
        }

        private static Student[] ListOfStudents => new[]
        {
            new Student()
            {
                StudentKey = 1,
                StudentFirstName = "Student",
                StudentLastName = "1"
            },
            new Student()
            {
                StudentKey = 2,
                StudentFirstName = "Student",
                StudentLastName = "2"
            },
            new Student()
            {
                StudentKey = 3,
                StudentFirstName = "Student",
                StudentLastName = "3"
            }
        };

        private static Goal[] ListOfGoals => new[]
        {
            new Goal
            {
                GoalKey = 1,
                GoalDescription = "I can speak respectfully.",
                GoalType = GoalType.YesNo,
                StudentKey = 1
            },
            new Goal
            {
                GoalKey = 2,
                GoalDescription = "I can act appropriately.",
                GoalType = GoalType.Numeric,
                StudentKey = 1
            },
            new Goal
            {
                GoalKey = 3,
                GoalDescription = "I am a numeric goal type",
                GoalType = GoalType.Numeric,
                StudentKey = 2
            },
            new Goal
            {
                GoalKey = 4,
                GoalDescription = "I am a yes/no goal type",
                GoalType = GoalType.YesNo,
                StudentKey = 3
            }
        };
    }
}