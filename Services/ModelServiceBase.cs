using AutoMapper;
using Contracts.Logger;
using Contracts.Repository;

namespace Services
{

    /// <summary>
    /// This is a base of the services that work with models
    /// </summary>

    public abstract class ModelServiceBase : ServiceBase
    {
        protected IMapper _mapper;
        protected ModelServiceBase(ILoggerManager logger, IRepositoryManager repository,IMapper mapper) : base(logger, repository)
        {
            _mapper = mapper;
        }
    }
}
