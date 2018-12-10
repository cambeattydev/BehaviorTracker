using System.Collections.Generic;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Models.Student> GetStudents();

        IEnumerable<Models.Student> GetStudentsWithGoalsAndAvailableAnswers();
    }
}