using Contracts.Models;
using Contracts.Repository.ModelRepositories;
using Entities.Context;

namespace Entities.Repository.ModelRepositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }

        public void Create(Order entity) => base.CreateEntity(entity);

        public void Delete(Order entity) => base.DeleteEntity(entity);

        public void Update(Order entity) => base.UpdateEntity(entity);
    }
}
