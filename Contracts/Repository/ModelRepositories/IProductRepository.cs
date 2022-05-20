using Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories
{
    public interface IProductRepository : ICrudBase<Product>
    {
        Task<Product> GetByIdAsync(long id,bool trackChanges);
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<long> ids, bool trackChanges);
    }

}
