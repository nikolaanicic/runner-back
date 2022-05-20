using Contracts.Repository.ModelRepositories;
using Contracts.Repository.ModelRepositories.UserRepositories;
using System.Threading.Tasks;

namespace Contracts.Repository
{

    /// <summary>
    /// This interface is the repo manager interface
    /// Service(class) of this type will be used as the repository in the service layer of the application
    /// Idea is that if a service needs to access multiple repositories at once it can do that easily 
    /// Using services of IRepositoryManager type
    /// </summary>
    public interface IRepositoryManager
    {
        IConsumerRepository Consumers { get; }
        IDelivererRepository Deliverers { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IAdminRepository Admins { get; }
        IUserRepository Users { get; }

        Task SaveAsync();
    }
}
