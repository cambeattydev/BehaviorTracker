namespace BehaviorTracker.Repository
{
    public static class SeedData
    {
        private static readonly IDictionary<BehaviorTrackerRoleGroups, BehaviorTrackerRoles[]>
            _roleGroupsrolesDictionary = new Dictionary<BehaviorTrackerRoleGroups, BehaviorTrackerRoles[]>
            {
                {
                    BehaviorTrackerRoleGroups.Admin,
                    Enum.GetValues(typeof(BehaviorTrackerRoles)).Cast<BehaviorTrackerRoles>()
                        .Where(roleName => roleName != BehaviorTrackerRoles.None).ToArray()
                },
                {
                    BehaviorTrackerRoleGroups.Teacher,
                    new[]
                    {
                        BehaviorTrackerRoles.AddGoalAnswerWriter, BehaviorTrackerRoles.GoalWrite,
                        BehaviorTrackerRoles.GoalAnswerWrite, BehaviorTrackerRoles.GoalAnswerRead
                    }
                },
                {
                    BehaviorTrackerRoleGroups.GoalAnswerWriter,
                    new[] {BehaviorTrackerRoles.GoalAnswerWrite, BehaviorTrackerRoles.GoalAnswerRead}
                },
                {BehaviorTrackerRoleGroups.Student, new[] {BehaviorTrackerRoles.GoalAnswerRead}}
            };

        internal static BehaviorTrackerRole[] Roles =>
            Enum.GetValues(typeof(BehaviorTrackerRoles)).Cast<BehaviorTrackerRoles>()
                .Where(roleName => roleName != BehaviorTrackerRoles.None).Select(roleName => new BehaviorTrackerRole
                {
                    BehaviorTrackerRoleKey = (long) roleName,
                    RoleName = roleName.ToString()
                }).ToArray();

        internal static BehaviorTrackerUser[] Users => new[]
        {
            new BehaviorTrackerUser
            {
                BehaviorTrackerUserKey = 1,
                FirstName = "Britney",
                LastName = "Beatty",
                Email = "bbeatty@cbcsd.org"
            },
        };

        internal static BehaviorTrackerRoleGroup[] RoleGroups =>
            Enum.GetValues(typeof(BehaviorTrackerRoleGroups)).Cast<BehaviorTrackerRoleGroups>()
                .Where(roleName => roleName != BehaviorTrackerRoleGroups.None).Select(roleGroupName =>
                    new BehaviorTrackerRoleGroup
                    {
                        BehaviorTrackerRoleGroupKey = (long) roleGroupName,
                        RoleGroupName = roleGroupName.ToString()
                    }).ToArray();

        internal static BehaviorTrackerRoleGroupRole[] RoleGroupRoles =>
            _roleGroupsrolesDictionary.SelectMany((kvp, keyIndex) =>
            {
                var startingCount = 0;
                for (var i = 0; i < keyIndex; i++)
                {
                    startingCount += _roleGroupsrolesDictionary[_roleGroupsrolesDictionary.Keys.ElementAt(i)].Length;
                }

                return kvp.Value.Select((role, roleIndex) => new BehaviorTrackerRoleGroupRole
                {
                    BehaviorTrackerRoleGroupRoleKey = startingCount + roleIndex + 1,
                    BehaviorTrackerRoleKey = (long) role,
                    BehaviorTrackerRoleGroupKey = (long) kvp.Key
                });
            }).ToArray();

        internal static BehaviorTrackerUserRoleGroup[] UserRoleGroups => new[]
        {
            new BehaviorTrackerUserRoleGroup
            {
                BehaviorTrackerUserRoleGroupKey = 1,
                BehaviorTrackerUserKey = 1,
                BehaviorTrackerRoleGroupKey = (long) BehaviorTrackerRoleGroups.Admin
            }
        };

        public static class Testing
        {
            internal static Goal[] Goals => new[]
            {
                new Goal
                {
                    GoalKey = 1,
                    GoalDescription = "I can speak respectfully.",
                    GoalType = GoalType.YesNo,
                    BehaviorTrackerUserKey = 3
                },
                new Goal
                {
                    GoalKey = 2,
                    GoalDescription = "I can act appropriately.",
                    GoalType = GoalType.Numeric,
                    BehaviorTrackerUserKey = 3
                },
                new Goal
                {
                    GoalKey = 3,
                    GoalDescription = "I am a numeric goal type",
                    GoalType = GoalType.Numeric,
                    BehaviorTrackerUserKey = 4
                },
                new Goal
                {
                    GoalKey = 4,
                    GoalDescription = "I am a yes/no goal type",
                    GoalType = GoalType.YesNo,
                    BehaviorTrackerUserKey = 5
                }
            };

            internal static GoalAvailableAnswer[] AvailableAnswers => new[]
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

            internal static BehaviorTrackerUser[] Users => new[]
            {
                new BehaviorTrackerUser
                {
                    BehaviorTrackerUserKey = 2,
                    FirstName = "Cameron",
                    LastName = "Beatty",
                    Email = "beatty2121@gmail.com"
                },
                new BehaviorTrackerUser
                {
                    BehaviorTrackerUserKey = 3,
                    FirstName = "BehaviorTracker",
                    LastName = "User 1",
                    Email = "BehaviorTrackerUser1@cbcsd.org"
                },
                new BehaviorTrackerUser
                {
                    BehaviorTrackerUserKey = 4,
                    FirstName = "BehaviorTracker",
                    LastName = "User 2",
                    Email = "BehaviorTrackerUser2@cbcsd.org"
                },
                new BehaviorTrackerUser
                {
                    BehaviorTrackerUserKey = 5,
                    FirstName = "BehaviorTracker",
                    LastName = "User 3",
                    Email = "BehaviorTrackerUser3@cbcsd.org"
                }
            };

            internal static BehaviorTrackerUserRoleGroup[] UserRoleGroups => new[]
            {
                new BehaviorTrackerUserRoleGroup
                {
                    BehaviorTrackerRoleGroupKey = (long) BehaviorTrackerRoleGroups.Admin,
                    BehaviorTrackerUserKey = 2,
                    BehaviorTrackerUserRoleGroupKey = 2
                },
                new BehaviorTrackerUserRoleGroup
                {
                    BehaviorTrackerRoleGroupKey = (long) BehaviorTrackerRoleGroups.Student,
                    BehaviorTrackerUserKey = 3,
                    BehaviorTrackerUserRoleGroupKey = 3
                },
                new BehaviorTrackerUserRoleGroup
                {
                    BehaviorTrackerRoleGroupKey = (long) BehaviorTrackerRoleGroups.Student,
                    BehaviorTrackerUserKey = 4,
                    BehaviorTrackerUserRoleGroupKey = 4
                },
                new BehaviorTrackerUserRoleGroup
                {
                    BehaviorTrackerRoleGroupKey = (long) BehaviorTrackerRoleGroups.Student,
                    BehaviorTrackerUserKey = 5,
                    BehaviorTrackerUserRoleGroupKey = 5
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
        }
    }
}