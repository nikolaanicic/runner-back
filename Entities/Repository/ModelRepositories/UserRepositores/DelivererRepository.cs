using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository.ModelRepositories.UserRepositores
{
    public class DelivererRepository : UserRepositoryBase<Deliverer>, IDelivererRepository
    {
        public DelivererRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Deliverer>> GetPendingDeliverers(bool trackChanges)
        {
            return await base.FindByCondition(x => x.State != ProfileState.APPROVED, trackChanges).ToListAsync();
        }
    }
}
