using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.Models;

namespace BehaviorTracker.Repository.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        IEnumerable<Student> GetStudentsWithGoalsAndAvailableAnswers();
        Task<Student> SaveAsync(Student student);
        Task<Student> DeleteStudentAsync(long studentKey);
    }
}