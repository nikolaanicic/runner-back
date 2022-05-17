using Contracts.Logger;
using Contracts.Repository;

namespace Services
{

    /// <summary>
    /// This class is the basis of all the services
    /// </summary>
    public abstract class ServiceBase
    {

        protected ILoggerManager _logger;
        protected IRepositoryManager _repository;


        public ServiceBase(ILoggerManager logger,IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;

        }
    }
}
