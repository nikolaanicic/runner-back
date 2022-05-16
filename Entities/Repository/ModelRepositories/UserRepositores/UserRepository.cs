using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Repository.ModelRepositories.UserRepositores
{
    public class UserRepository :UserRepositoryBase<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetWithRole(string username, bool trackChanges) =>
            await base.GetEntityEager(u => u.Username == username, trackChanges, new string[1] { "Role" }).FirstOrDefaultAsync();
    }
}
