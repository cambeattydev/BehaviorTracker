using System.Linq;
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
            CreateMap<Client.Models.AuthorizationModel, Service.Models.AuthorizationModel>()
                .ForAllMembers(mo => mo.Ignore());

            CreateMap<Service.Models.GoalAnswerScore, Client.Models.GoalAnswerScore>();
            CreateMap<Service.Models.GoalAnswerTotal, Client.Models.GoalAnswerTotal>();

            CreateMap<Service.Models.AuthorizationModel, Client.Models.AuthorizationModel>()
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.Condition(src => src.User != null);
                    opt.MapFrom(src => src.User.Email);
                })
                .ForMember(dest => dest.FirstName, opt =>
                {
                    opt.Condition(src => src.User != null);
                    opt.MapFrom(src => src.User.FirstName);
                })
                .ForMember(dest => dest.LastName, opt =>
                {
                    opt.Condition(src => src.User != null);
                    opt.MapFrom(src => src.User.LastName);
                })
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
        }
    }
}