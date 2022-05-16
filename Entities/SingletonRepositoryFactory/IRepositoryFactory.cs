
using Entities.Context;

namespace Entities.SingletonRepositoryFactory
{
    public interface IRepositoryFactory
    {
        T GetInstance<T>(DatabaseContext context) where T:class;
    }
}
