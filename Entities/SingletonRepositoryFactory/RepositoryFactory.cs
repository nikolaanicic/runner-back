using Contracts.Repository.ModelRepositories;
using Contracts.Repository.ModelRepositories.UserRepositories;
using Entities.Context;
using Entities.Repository.ModelRepositories;
using Entities.Repository.ModelRepositories.UserRepositores;
using System;
using System.Collections.Generic;

namespace Entities.SingletonRepositoryFactory
{

    /// <summary>
    /// This is a class that implements IRepositoryFactory
    /// The implementation should provide a way to get the desired repository instance if it already exists
    /// And if it doesn't exist the factory should in a thread-safe way instantiate the appropriate repository implementation
    /// For that this class uses three dictionaries
    /// locks - the dictionary that has <repo_interface_type,lock_object> as the dataset
    /// instances - has <repo_interface_type,concrete_repo> as the dataset
    /// mappings - <repo_interface_type,repo_implementaion_type> as the dataset
    /// 
    /// Each of the repo_implementation_type types must have a constructor of the following signature: Repo(DatabaseContext)
    /// If one of the types doesn't have that constructor the code will throw an exception
    /// 
    /// </summary>

    public class RepositoryFactory : IRepositoryFactory
    {

        private Dictionary<Type, object> instances;
        private Dictionary<Type, object> locks;
        private Dictionary<Type, Type> mappings;

        public RepositoryFactory()
        {
            instances = new Dictionary<Type, object>();
            locks = new Dictionary<Type, object>();
            mappings = new Dictionary<Type, Type>();

            SetUpMappings();
            SetUpLocks();
            SetUpInstances();
        }


        /// <summary>
        /// This method provides the implementation that gets the wanted repository
        /// It uses double locking pattern to instantiate singleton repo if it doesn't exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>

        public T GetInstance<T>(DatabaseContext context) where T : class
        {

            // taking the type of the generic parameter and checking if it is recognized by the factory
            // if the type is not recognized i.e it is not previously registered throw an exception
            Type t = typeof(T);
            if (!locks.ContainsKey(t) || !mappings.ContainsKey(t))
                throw new KeyNotFoundException($"Key {t} is not found");

            // gets the lock for the appropriate type and the mapping
            object toLock = locks[t];
            Type concreteType = mappings[t];

            if(!instances.ContainsKey(t) || instances[t] == null)
            {
                lock(toLock)
                {
                    if (!instances.ContainsKey(t) || instances[t] == null)
                    {

                        // getting the needed constructor of the specific signature needed
                        // to instantiate the repository
                        // check RepositoryBase class
                        // if the constructor is null (should never be null) throw an exception

                        var constructor = concreteType.GetConstructor(new Type[1] { typeof(DatabaseContext) });

                        if (constructor == null)
                            throw new NullReferenceException("Type constructor doesn't exist");


                        // create a new instance of the repository and store it in the dictionary
                        instances[t] = constructor.Invoke(new object[1] { context });
                    }
                }
            }

            return (T)(instances[t]);
        }


        // prepare instance spaces in the dictionary. 
        // this method is not needed because a of !instances.ContainsKey(type)
        private void SetUpInstances()
        {

            instances[typeof(IUserRepository)] = null;
            instances[typeof(IAdminRepository)] = null;
            instances[typeof(IConsumerRepository)] = null;
            instances[typeof(IDelivererRepository)] = null;

            instances[typeof(IProductRepository)] = null;
            instances[typeof(IOrderRepository)] = null;
        }


        private void SetUpLocks()
        {
            locks[typeof(IUserRepository)] = new object();
            locks[typeof(IAdminRepository)] = new object();
            locks[typeof(IConsumerRepository)] = new object();
            locks[typeof(IDelivererRepository)] = new object();

            locks[typeof(IProductRepository)] = new object();
            locks[typeof(IOrderRepository)] = new object();
        }


        private void SetUpMappings()
        {
            mappings[typeof(IUserRepository)] = typeof(UserRepository);
            mappings[typeof(IAdminRepository)] = typeof(AdminRepository);
            mappings[typeof(IConsumerRepository)] = typeof(ConsumerRepository);
            mappings[typeof(IDelivererRepository)] = typeof(DelivererRepository);

            mappings[typeof(IProductRepository)] = typeof(ProductRepository);
            mappings[typeof(IOrderRepository)] = typeof(OrderRepository);
        }
    }
}
