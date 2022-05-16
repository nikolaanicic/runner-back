using Contracts.Models;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;

namespace Entities.Repository.ModelRepositories.UserRepositores
{
    public class ConsumerRepository :UserRepositoryBase<Consumer>, IConsumerRepository
    {
        public ConsumerRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
