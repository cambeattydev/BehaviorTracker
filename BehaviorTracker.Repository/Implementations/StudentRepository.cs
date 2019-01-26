using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BehaviorTracker.Repository.DatabaseModels;
using BehaviorTracker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace BehaviorTracker.Repository.Implementations
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext) : base(
            behaviorTrackerDatabaseContext)
        {
        }

        public IEnumerable<Student> GetStudents()
        {
            return _behaviorTrackerDatabaseContext.Students.Include(s => s.Goals).AsEnumerable();
        }

        public IEnumerable<Student> GetStudentsWithGoalsAndAvailableAnswers()
        {
            return _behaviorTrackerDatabaseContext.Students.Include(s => s.Goals).ThenInclude(s => s.AvailableAnswers);
        }

        public async Task<Student> SaveAsync(Student student)
        {
            var savedStudent = student.StudentKey < 1
                ? _behaviorTrackerDatabaseContext.Students.Add(student)
                : _behaviorTrackerDatabaseContext.Students.Update(student);

            await _behaviorTrackerDatabaseContext.SaveChangesAsync().ConfigureAwait(false);
            return savedStudent.Entity;
        }

        public async Task<Student> DeleteStudentAsync(long studentKey)
        {
            var studentToDelete = await _behaviorTrackerDatabaseContext.Students
                .FirstOrDefaultAsync(student => student.StudentKey == studentKey).ConfigureAwait(false);
            if (studentToDelete != null)
            {
                _behaviorTrackerDatabaseContext.Students.Remove(studentToDelete);
                await _behaviorTrackerDatabaseContext.SaveChangesAsync().ConfigureAwait(false);

                return studentToDelete;
            }

            return null;
        }
    }
}