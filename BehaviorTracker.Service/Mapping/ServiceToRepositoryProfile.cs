using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BehaviorTracker.Service.Models;
using BehaviorTracker.Shared;

namespace BehaviorTracker.Service.Mapping
{
    public class ServiceToRepositoryProfile : Profile
    {
        public ServiceToRepositoryProfile()
        {
            CreateMap<Goal, Repository.Models.Goal>();
            CreateMap<GoalAnswer, Repository.Models.GoalAnswer>();
            CreateMap<GoalAvailableAnswer, Repository.Models.GoalAvailableAnswer>();
            CreateMap<Student, Repository.Models.Student>();
            CreateMap<GoalAnswerScore, IEnumerable<Repository.Models.GoalAnswerScore>>()
                .ForAllMembers(mo => mo.Ignore());

            CreateMap<IEnumerable<Repository.Models.GoalAnswerScore>, GoalAnswerScore>()
                .ForMember(dest => dest.Available, mo => mo.MapFrom(src => src.Sum(s => s.MaxValue)))
                .ForMember(dest => dest.Received, mo =>
                    mo.MapFrom(src => src.Sum(s =>
                        s.GoalType == GoalType.Numeric ? float.Parse(s.Goal.Answer) :
                        bool.Parse(s.Goal.Answer) ? 1 : 0)));
        }
    }
}