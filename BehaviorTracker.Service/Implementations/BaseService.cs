using AutoMapper;

namespace BehaviorTracker.Service.Implementations
{
    public class BaseService
    {
        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
        internal IMapper _mapper;
    }
}