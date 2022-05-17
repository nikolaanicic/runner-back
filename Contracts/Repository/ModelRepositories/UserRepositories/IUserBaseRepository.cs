using Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories.UserRepositories
{
    /// <summary>
    /// This interface is the base of the user repository interfaces
    /// Since all user repos will have similar methods they are grouped in this interface
    /// </summary>
    /// <typeparam name="T"></typeparam>


    public interface IUserBaseRepository<T>:ICrudBase<T>
        where T:User
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges);
        Task<T> GetByUsernameAsync(string username, bool trackChanges);
        Task<T> GetByEmailAsync(string email, bool trackChanges);
    }
}
