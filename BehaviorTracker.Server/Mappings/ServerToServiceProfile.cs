using AutoMapper;

namespace BehaviorTracker.Server.Mappings
{
    public class ServerToServiceProfile : Profile
    {
        public ServerToServiceProfile()
        {
            CreateMap<Client.Models.Goal, Service.Models.Goal>();
            CreateMap<Client.Models.GoalAnswer, Service.Models.GoalAnswer>();
            CreateMap<Client.Models.GoalAvailableAnswer, Service.Models.GoalAvailableAnswer>();
            CreateMap<Client.Models.Student, Service.Models.Student>();
        }
    }
}