using System.Collections.Generic;
using System.Linq;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace BehaviorTracker.Repository.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly BehaviorTrackerDatabaseContext _dbContext;
        public StudentRepository(BehaviorTrackerDatabaseContext behaviorTrackerDatabaseContext)
        {
            _dbContext = behaviorTrackerDatabaseContext;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students.Include(s => s.Goals);
        }
        
        public IEnumerable<Student> GetStudentsWithGoalsAndAvailableAnswers()
        {
            return _dbContext.Students.Include(s => s.Goals).ThenInclude(s => s.AvailableAnswers);
        }
    }
}