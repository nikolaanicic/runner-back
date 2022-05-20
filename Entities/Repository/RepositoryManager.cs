using Contracts.Repository;
using Contracts.Repository.ModelRepositories;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;
using Entities.SingletonRepositoryFactory;
using System.Threading.Tasks;

namespace Entities.Repository
{

    /// <summary>
    /// This is the implementation of the repo manager
    /// This class should merely provide a way to get the implementations of all the repositories
    /// To do that it uses a factory
    /// </summary>
    public class RepositoryManager : IRepositoryManager
    {
        private DatabaseContext _context;
        private IRepositoryFactory _factory;

        public RepositoryManager(DatabaseContext context)
        {
            _context = context;
            _factory = new RepositoryFactory();
        }

        public IConsumerRepository Consumers => _factory.GetInstance<IConsumerRepository>(_context);

        public IDelivererRepository Deliverers => _factory.GetInstance<IDelivererRepository>(_context);

        public IProductRepository Products => _factory.GetInstance<IProductRepository>(_context);

        public IOrderRepository Orders => _factory.GetInstance<IOrderRepository>(_context);

        public IAdminRepository Admins => _factory.GetInstance<IAdminRepository>(_context);

        public IUserRepository Users => _factory.GetInstance<IUserRepository>(_context);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
