using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;
using GoalAnswer = BehaviorTracker.Service.Models.GoalAnswer;

namespace BehaviorTracker.Service.Implementations
{
    public class GoalAnswerService : BaseService, IGoalAnswerService
    {
        private readonly IGoalAnswerRepository _goalAnswerRepository;
        private readonly Dictionary<DayOfWeek, int> _daysToAddToGetMonday =new Dictionary<DayOfWeek, int>
        {
            {DayOfWeek.Monday, 0},
            {DayOfWeek.Tuesday, -1},
            {DayOfWeek.Wednesday, -2},
            {DayOfWeek.Thursday, -3},
            {DayOfWeek.Friday, -4},
            {DayOfWeek.Saturday, -5},
            {DayOfWeek.Sunday, -6}
        };

        public GoalAnswerService(IMapper mapper, IGoalAnswerRepository goalAnswerRepository) : base(mapper)
        {
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

        /// <inheritdoc />
        public async Task<GoalAnswer> DeleteAsync(long goalAnswerKey)
        {
            var deletedGoalAnswer = await _goalAnswerRepository.DeleteAsync(goalAnswerKey);
            var mappedDeletedGoalAnswer = _mapper.Map<GoalAnswer>(deletedGoalAnswer);
            return mappedDeletedGoalAnswer;
        }

        public Models.GoalAnswerTotal GoalAnswersTotal(long goalKey, DateTime date)
        {
            try
            {
                var goalAnswerScores = _goalAnswerRepository.GoalAnswersScore(goalKey, date);
                var mappedGoalAnswerScores = _mapper.Map<Models.GoalAnswerScore>(goalAnswerScores);

                var mondayDate = GetMondayDate(date);
                var weeklyGoalAnswerScores = _goalAnswerRepository.WeeklyGoalAnswersScore(goalKey, mondayDate);
                var mappedWeeklyGoalAnswerScores = _mapper.Map<Models.GoalAnswerScore>(weeklyGoalAnswerScores);
                return new GoalAnswerTotal
                {
                    Daily = mappedGoalAnswerScores,
                    Weekly = mappedWeeklyGoalAnswerScores
                };
            }
            catch (Exception ex)
            {
                var test = ex;
                throw;
            }

        }

        private DateTime GetMondayDate(DateTime date) => date.AddDays(_daysToAddToGetMonday[date.DayOfWeek]);
    }
}