using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;

namespace Entities.Repository.ModelRepositories.UserRepositores
{
    public class AdminRepository : UserRepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
