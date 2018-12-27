using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BehaviorTracker.Server.Controllers
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Client.Models.Student> Students() => _studentService.GetStudents().Select(_mapper.Map<Client.Models.Student>);

        [HttpGet("[action]")]
        public IEnumerable<Client.Models.Student> GetStudentsWithGoalsAndAvailableAnswers() =>
            _studentService.GetStudentsWithGoalsAndAvailableAnswers().Select(_mapper.Map<Client.Models.Student>);

        [HttpPost("[action]")]
        public async Task<IActionResult> Student([FromBody] Client.Models.Student studentModel)
        {
            
            var student = _mapper.Map<Service.Models.Student>(studentModel);
            if (student == null)
            {
                return BadRequest();
            }

            var savedStudent  = await _studentService.SaveAsync(student).ConfigureAwait(false);
            _mapper.Map<Client.Models.Student>(savedStudent);
            return Ok(savedStudent);
        }
        
        [HttpDelete("[action]/{studentKey}")]
        public async Task<IActionResult> Delete(long studentKey)
        {
            var deletedStudent = await _studentService.DeletedAsync(studentKey);
            if (deletedStudent == null)
            {
                return BadRequest();
            }

            var mappedDeletedStudent = _mapper.Map<Client.Models.Student>(deletedStudent);
            return Ok(mappedDeletedStudent);
        }
    }
}