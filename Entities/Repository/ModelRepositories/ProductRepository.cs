using Contracts.Models;
using Contracts.Repository.ModelRepositories;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Repository.ModelRepositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }

        public void Create(Product entity) => base.CreateEntity(entity);

        public void Delete(Product entity) => base.DeleteEntity(entity);

        public async Task<IEnumerable<Product>> GetAllAsync(bool trackChanges) =>
            await base.FindAll(trackChanges).OrderBy(p=>p.Id).ToListAsync();

        public async Task<Product> GetByIdAsync(long id, bool trackChanges) =>
            await base.FindByCondition(p => p.Id == id, trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<long> ids, bool trackChanges) =>
            await base.FindByCondition(p => ids.Contains(p.Id), trackChanges).OrderBy(p => p.Id).ToListAsync();

        public void Update(Product entity) => base.UpdateEntity(entity);
    }
}
