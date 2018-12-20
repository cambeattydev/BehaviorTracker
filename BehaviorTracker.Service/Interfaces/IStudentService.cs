using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Models.Student> GetStudents();

        IEnumerable<Models.Student> GetStudentsWithGoalsAndAvailableAnswers();
        Task<Student> SaveAsync(Student student);
    }
}