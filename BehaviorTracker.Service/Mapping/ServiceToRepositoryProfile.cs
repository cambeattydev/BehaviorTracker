using AutoMapper;
using BehaviorTracker.Service.Models;

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
        }
    }
}