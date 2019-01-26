using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Implementations
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IMapper mapper, IStudentRepository studentRepository) : base(mapper)
        {
            _studentRepository = studentRepository;
        }

        public IEnumerable<Models.Student> GetStudents()
        {
            var repositoryStudents = _studentRepository.GetStudents();
            return repositoryStudents.Select(_mapper.Map<Models.Student>);
        }

        public IEnumerable<Student> GetStudentsWithGoalsAndAvailableAnswers()
        {
            return _studentRepository.GetStudentsWithGoalsAndAvailableAnswers().Select(_mapper.Map<Models.Student>);
        }

        public async Task<Student> SaveAsync(Student student)
        {
            var repositoryStudent = _mapper.Map<Repository.Models.Student>(student);
            var savedStudent = await _studentRepository.SaveAsync(repositoryStudent).ConfigureAwait(false);
            return _mapper.Map<Student>(savedStudent);
        }

        public async Task<Student> DeleteAsync(long studentKey)
        {
            var repositoryStudent = await _studentRepository.DeleteStudentAsync(studentKey).ConfigureAwait(false);
            var deletedStudent = _mapper.Map<Models.Student>(repositoryStudent);
            return deletedStudent;
        }
    }
}