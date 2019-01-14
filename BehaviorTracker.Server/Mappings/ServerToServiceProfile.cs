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
            CreateMap<Client.Models.GoalAnswerScore, Service.Models.GoalAnswerScore>()
                .ForAllMembers(mo => mo.Ignore());
            CreateMap<Client.Models.GoalAnswerTotal, Service.Models.GoalAnswerTotal>()
                .ForAllMembers(mo => mo.Ignore());

            CreateMap<Service.Models.GoalAnswerScore, Client.Models.GoalAnswerScore>();
            CreateMap<Service.Models.GoalAnswerTotal, Client.Models.GoalAnswerTotal>();
        }
    }
}