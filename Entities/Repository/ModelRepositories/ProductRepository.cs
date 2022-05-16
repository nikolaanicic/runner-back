using Contracts.Models;
using Contracts.Repository.ModelRepositories;
using Entities.Context;

namespace Entities.Repository.ModelRepositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }

        public void Create(Product entity) => base.CreateEntity(entity);

        public void Delete(Product entity) => base.DeleteEntity(entity);

        public void Update(Product entity) => base.UpdateEntity(entity);
    }
}
