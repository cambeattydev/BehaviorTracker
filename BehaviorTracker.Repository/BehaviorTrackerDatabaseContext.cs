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
        
        internal DbSet<BehaviorTrackerUserRoles> BehaviorTrackerUserRoles { get; set; }
        
        internal DbSet<BehaviorTrackerRole> BehaviorTrackerRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

//#if DEBUG
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Student>().HasData(ListOfStudents);
//            modelBuilder.Entity<Goal>().HasData(ListOfGoals);
//            modelBuilder.Entity<GoalAvailableAnswer>().HasData(_availableAnswers);
//            //Creates goalAnswer test data for the time you are testing
//            var currentHour = DateTime.Now.Hour > 15 ? 15 : DateTime.Now.Hour < 8 ? 8 : DateTime.Now.Hour;
//            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentHour,
//                currentHour >= 15 || currentHour == 8 ? 30 : DateTime.Now.Minute >= 30 ? 30 : 0, 0);
//            modelBuilder.Entity<GoalAnswer>().HasData(ListOfGoalAnswers(date));
//        }
//#endif

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

        private static GoalAnswer[] ListOfGoalAnswers(DateTime date) => new[]
        {
            new GoalAnswer
            {
                GoalAnswerKey = 1,
                Answer = true.ToString(),
                GoalKey = 1,
                Date = date,
            },
            new GoalAnswer
            {
                GoalAnswerKey = 2,
                GoalKey = 2,
                Answer = "1",
                Date = date
            },
            new GoalAnswer
            {
                GoalAnswerKey = 3,
                GoalKey = 3,
                Answer = "2",
                Date = date
            },
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

        private static GoalAvailableAnswer[] _availableAnswers => new[]
        {
            //Goal 2
            new GoalAvailableAnswer
            {
                GoalKey = 2,
                GoalAvailableAnswerKey = 1,
                OptionValue = "1"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 2,
                GoalAvailableAnswerKey = 2,
                OptionValue = "2"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 2,
                GoalAvailableAnswerKey = 3,
                OptionValue = "3"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 2,
                GoalAvailableAnswerKey = 4,
                OptionValue = "4"
            },
            //Goal 3
            new GoalAvailableAnswer
            {
                GoalKey = 3,
                GoalAvailableAnswerKey = 5,
                OptionValue = "0"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 3,
                GoalAvailableAnswerKey = 6,
                OptionValue = "0.5"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 3,
                GoalAvailableAnswerKey = 7,
                OptionValue = "1"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 3,
                GoalAvailableAnswerKey = 8,
                OptionValue = "1.5"
            },
            new GoalAvailableAnswer
            {
                GoalKey = 3,
                GoalAvailableAnswerKey = 9,
                OptionValue = "2"
            },
        };
    }
}