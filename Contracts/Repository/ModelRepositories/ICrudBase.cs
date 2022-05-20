using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repository.ModelRepositories
{
   public interface ICrudBase<T>
   {
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);

        Task<IEnumerable<T>> GetAllAsync(bool trackChanges);
   }
}
