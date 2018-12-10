using System.Collections.Generic;
using System.Linq;
using BehaviorTracker.Repository.Models;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        IEnumerable<Student> GetStudentsWithGoalsAndAvailableAnswers();
    }
}