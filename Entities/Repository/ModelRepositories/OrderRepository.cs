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
            await base.GetEntitiesEager(o=>o.Id==o.Id,trackChanges, new string[3] { "Produce", "Deliverer", "Consumer" }).OrderBy(o => o.Id).ToListAsync();

        public async Task<Order> GetByIdAsync(long id, bool trackChanges) =>
            await base.FindByCondition(o => o.Id == id, trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Order>> GetActiveAsync(bool trackChanges) =>
            await base.GetEntitiesEager(o => o.OrderStatus == OrderStatus.WAITING && o.Deliverer == null, trackChanges,new string[3] { "Produce","Deliverer","Consumer" }).OrderBy(o=>o.Id).ToListAsync();

        public async Task<IEnumerable<Order>> GetCompletedByConsumerAsync(long id, bool trackChanges) =>
            await base.GetEntitiesEager(o => o.OrderStatus == OrderStatus.DELIVERED && o.ConsumerId == id, trackChanges, new string[3] { "Produce", "Deliverer", "Consumer" })
            .OrderBy(o => o.Id).ToListAsync();
 
        public async Task<IEnumerable<Order>> GetCompletedByDelivererAsync(long id, bool trackChanges) =>
            await base.GetEntitiesEager(o => o.OrderStatus == OrderStatus.DELIVERED && o.DelivererId == id, trackChanges, new string[3] { "Produce", "Deliverer", "Consumer" })
            .OrderBy(o => o.Id).ToListAsync();

        public void Update(Order entity) => base.UpdateEntity(entity);
    }
}
