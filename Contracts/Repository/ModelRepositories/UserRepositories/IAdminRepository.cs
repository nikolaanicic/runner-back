using Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories.UserRepositories
{
    public interface IAdminRepository : IUserBaseRepository<Admin>
    {
    }
}
