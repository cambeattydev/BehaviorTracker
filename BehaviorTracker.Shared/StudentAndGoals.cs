using System.Collections.Generic;

namespace BehaviorTracker.Shared
{
    public class StudentAndGoals
    {
        public StudentAndGoals(){}
        public StudentAndGoals(string studentName, IEnumerable<GoalResponse> goals)
        {
            StudentName = studentName;
            Goals = goals;
        }
        
        public string StudentName { get; set; }
        
        public IEnumerable<GoalResponse> Goals { get; set; }
    }
}