using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BehaviorTracker.Repository.Interfaces;
using BehaviorTracker.Service.Interfaces;
using BehaviorTracker.Service.Models;

namespace BehaviorTracker.Service.Implementations
{
    public class GoalAvailableAnswerService : IGoalAvailableAnswerService
    {
        private IGoalAvailableAnswerRepository _goalAvailableAnswerRepository;
        private IMapper _mapper;

        public GoalAvailableAnswerService(IGoalAvailableAnswerRepository goalAvailableAnswerRepository, IMapper mapper)
        {
            _goalAvailableAnswerRepository = goalAvailableAnswerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GoalAvailableAnswer>> DeleteAndInsertAsync(
            IEnumerable<GoalAvailableAnswer> goalAvailableAnswers)
        {
            if (goalAvailableAnswers == null)
            {
                throw new ArgumentNullException(nameof(goalAvailableAnswers));
            }

            var mappedGoalAvailableAnswers =
                goalAvailableAnswers.Select(_mapper.Map<Repository.Models.GoalAvailableAnswer>).ToList();
            //Delete all the old available an
            if (mappedGoalAvailableAnswers.All(goalAvailableAnswer => goalAvailableAnswer.GoalAvailableAnswerKey < 1))
            {
                await Task.WhenAll(mappedGoalAvailableAnswers
                        .GroupBy(goalAvailableAnswer => goalAvailableAnswer.GoalKey)
                        .Select(goalAvailableAnswer =>
                            _goalAvailableAnswerRepository.DeleteAllForGoalAsync(goalAvailableAnswer.Key)))
                    .ConfigureAwait(false);
            }

            var savedGoalAvailableAnswers = await _goalAvailableAnswerRepository.InsertAsync(mappedGoalAvailableAnswers);
            return savedGoalAvailableAnswers.Select(_mapper.Map<GoalAvailableAnswer>);
        }

        public async Task<IEnumerable<GoalAvailableAnswer>> DeleteAllForGoal(long goalKey)
        {
            var deletedGoalAvailableAnswers =
                await _goalAvailableAnswerRepository.DeleteAllForGoalAsync(goalKey).ConfigureAwait(false);
            return deletedGoalAvailableAnswers.Select(_mapper.Map<GoalAvailableAnswer>);
        }
    }
}