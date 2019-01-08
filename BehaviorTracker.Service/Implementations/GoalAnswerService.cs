using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Repository.Models;
using BehaviorTracker.Service.Interfaces;

namespace BehaviorTracker.Service.Implementations
{
    public class GoalAnswerService : IGoalAnswerService
    {
        private readonly IMapper _mapper;
        private readonly IGoalAnswerRepository _goalAnswerRepository;

        public GoalAnswerService(IMapper mapper, IGoalAnswerRepository goalAnswerRepository)
        {
            _mapper = mapper;
            _goalAnswerRepository = goalAnswerRepository;
        }
        public async Task<IDictionary<long, Service.Models.GoalAnswer>> GetStudentGoalAnswers(long studentKey, DateTime dateTime)
        {
            var repositoryStudentGoalAnswers = await _goalAnswerRepository.GetStudentGoalAnswers(studentKey, dateTime);
            return repositoryStudentGoalAnswers.ToDictionary(goalAnswer => goalAnswer.Key,
                kvp => _mapper.Map<Service.Models.GoalAnswer>(kvp.Value));
        }

        public async Task<Service.Models.GoalAnswer> SaveAsync(Service.Models.GoalAnswer goalAnswer)
        {
            var mappedGoalAnswer = _mapper.Map<Repository.Models.GoalAnswer>(goalAnswer);
            var savedGoalAnswer = await _goalAnswerRepository.SaveAsync(mappedGoalAnswer);
            return _mapper.Map<Service.Models.GoalAnswer>(savedGoalAnswer);
        }
    }
}