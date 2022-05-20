using Contracts.Models;
using Contracts.Repository.ModelRepositories;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Repository.ModelRepositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }

        public void Create(Order entity) => base.CreateEntity(entity);

        public void Delete(Order entity) => base.DeleteEntity(entity);

        public async Task<IEnumerable<Order>> GetAllAsync(bool trackChanges) =>
            await base.FindAll(trackChanges).OrderBy(o => o.Id).ToListAsync();

        public async Task<Order> GetByIdAsync(long id, bool trackChanges) =>
            await base.FindByCondition(o => o.Id == id, trackChanges).FirstOrDefaultAsync();

        public void Update(Order entity) => base.UpdateEntity(entity);
    }
}
