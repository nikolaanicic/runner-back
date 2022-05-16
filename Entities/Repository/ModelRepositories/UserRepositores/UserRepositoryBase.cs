using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Repository.ModelRepositories.UserRepositores
{

    /// <summary>
    /// This class is the second layer of the user subhierarchy 
    /// Idea is similar to the BaseRepository class
    /// This class provides implementations needed by the user repositories 
    /// And those implementations should be adapted to the needed interfaces
    /// 
    /// example:
    /// 
    /// UserRepository: UserRepositoryBase<User>, IUserRepository
    /// 
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UserRepositoryBase<T> : RepositoryBase<T>, IUserBaseRepository<T>
        where T : User
    {
        protected UserRepositoryBase(DatabaseContext context) : base(context)
        {
        }

        public void Create(T user) => base.CreateEntity(user);

        public void Delete(T user) => base.DeleteEntity(user);
        public void Update(T user) => base.UpdateEntity(user);

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges) =>
            await base.FindAll(trackChanges).OrderBy(u => u.Username).ToListAsync();

        public async Task<T> GetAsync(string username, bool trackChanges) =>
            await base.FindByCondition(u => u.Username == username, trackChanges).FirstOrDefaultAsync();

    }
}
