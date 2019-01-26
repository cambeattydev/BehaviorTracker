using System;
using System.Linq;
using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Repository
{
    public static class SeedData
    {
        internal static BehaviorTrackerRole[] ListOfRoles =>
            Enum.GetValues(typeof(BehaviorTrackerRoles)).Cast<BehaviorTrackerRoles>().Where(roleName => roleName != BehaviorTrackerRoles.None).Select(roleName => new BehaviorTrackerRole
            {
                BehaviorTrackerRoleKey = (long) roleName,
                RoleName = roleName.ToString()
            }).ToArray();

        internal static BehaviorTrackerUser[] ListOfUsers => new[]
        {
            new BehaviorTrackerUser
            {
                BehaviorTrackerUserKey = 1,
                FirstName = "Britney",
                LastName = "Beatty",
                Email = "bbeatty@cbcsd.org"
            },
        };

        internal static BehaviorTrackerUserRole[] ListOfBehaviorTrackerUserRole => Enum.GetValues(typeof(BehaviorTrackerRoles))
            .Cast<long>().Where(roleKey => roleKey > 0).Select((roleKey, index) => new BehaviorTrackerUserRole
            {
                BehaviorTrackerUserRoleKey = ++index,
                BehaviorTrackerRoleKey = roleKey,
                BehaviorTrackerUserKey = 1
            }).ToArray();
        public static class Testing
        {
            internal static Student[] ListOfStudents => new[]
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

            internal static GoalAnswer[] ListOfGoalAnswers(DateTime date) => new[]
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

            internal static Goal[] ListOfGoals => new[]
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

            internal static GoalAvailableAnswer[] ListOfAvailableAnswers => new[]
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
            
            internal static BehaviorTrackerUser[] ListOfUsers => new[]
            {
                new BehaviorTrackerUser
                {
                    BehaviorTrackerUserKey = 2,
                    FirstName = "Cameron",
                    LastName = "Beatty",
                    Email = "beatty2121@gmail.com"
                },
            };
            
            internal static BehaviorTrackerUserRole[] ListOfBehaviorTrackerUserRole => Enum.GetValues(typeof(BehaviorTrackerRoles))
                .Cast<long>().Where(roleKey => roleKey > 0).Select((roleKey, index) => new BehaviorTrackerUserRole
                {
                    BehaviorTrackerUserRoleKey = index + SeedData.ListOfBehaviorTrackerUserRole.Length + 1,
                    BehaviorTrackerRoleKey = roleKey,
                    BehaviorTrackerUserKey = 2
                }).ToArray();
        }
    }
}