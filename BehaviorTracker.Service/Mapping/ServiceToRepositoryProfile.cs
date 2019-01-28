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
            CreateMap<Goal, Repository.DatabaseModels.Goal>();
            CreateMap<GoalAnswer, Repository.DatabaseModels.GoalAnswer>();
            CreateMap<GoalAvailableAnswer, Repository.DatabaseModels.GoalAvailableAnswer>();
            CreateMap<Student, Repository.DatabaseModels.Student>();
            CreateMap<BehaviorTrackerUser, Repository.DatabaseModels.BehaviorTrackerUser>();
            CreateMap<GoalAnswerScore, IEnumerable<Repository.OtherModels.GoalAnswerScore>>()
                .ForAllMembers(mo => mo.Ignore());
            CreateMap<BehaviorTrackerRoleGroup, Repository.DatabaseModels.BehaviorTrackerRoleGroup>();
            CreateMap<BehaviorTrackerRole, Repository.DatabaseModels.BehaviorTrackerRole>();

            CreateMap<IEnumerable<Repository.OtherModels.GoalAnswerScore>, GoalAnswerScore>()
                .ForMember(dest => dest.Available, mo => mo.MapFrom(src => src.Sum(s => s.MaxValue)))
                .ForMember(dest => dest.Received, mo =>
                    mo.MapFrom(src => src.Sum(s =>
                        s.GoalType == GoalType.Numeric ? float.Parse(s.Goal.Answer) :
                        bool.Parse(s.Goal.Answer) ? 1 : 0)));

            CreateMap<BehaviorTrackerUsersResponse, Repository.OtherModels.BehaviorTrackerUsersResponse>();
        }
    }
}