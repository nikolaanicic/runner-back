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

        public async Task<User> GetWithRoleByEmailAsync(string email, bool trackChanges) =>
            await base.GetEntitiesEager(u => u.Email == email, trackChanges, new string[1] { "Role" }).FirstOrDefaultAsync();
    }
}
