using AutoMapper;
using Contracts.Logger;
using Contracts.Repository;

namespace Services
{
    public abstract class ModelServiceBase : ServiceBase
    {
        protected IMapper _mapper;
        protected ModelServiceBase(ILoggerManager logger, IRepositoryManager repository,IMapper mapper) : base(logger, repository)
        {
            _mapper = mapper;
        }
    }
}
