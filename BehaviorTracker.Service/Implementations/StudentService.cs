using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        public StudentService(IMapper mapper, IStudentRepository studentRepository)
        {
            _mapper = mapper;
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
    }

   
}