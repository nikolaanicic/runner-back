using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;

namespace Entities.Repository.ModelRepositories.UserRepositores
{
    public class DelivererRepository : UserRepositoryBase<Deliverer>, IDelivererRepository
    {
        public DelivererRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
