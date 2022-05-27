using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository.ModelRepositories.UserRepositores
{
    public class DelivererRepository : UserRepositoryBase<Deliverer>, IDelivererRepository
    {
        public DelivererRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
