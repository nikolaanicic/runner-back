using Contracts.Models;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories.UserRepositories
{
    public interface IUserRepository : IUserBaseRepository<User>
    {
        Task<User> GetWithRoleByEmailAsync(string email, bool trackChanges);
    }
}
