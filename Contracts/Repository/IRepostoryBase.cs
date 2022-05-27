using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts.Repository
{
    public interface IRepostoryBase<T>
    {

        /// <summary>
        /// Should be implemented in a repository class in such a way that it gets all of the T entites
        /// </summary>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        IQueryable<T> FindAll(bool trackChanges);

        /// <summary>
        /// Should be implemented so that it gets all of the T entities that satisfy the condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges);

        /// <summary>
        /// Should be implemented so that it gets all of the T entities that satisfy the condition and have children attached to them
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trackChanges"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        IQueryable<T> GetEntitiesEager(Expression<Func<T, bool>> condition, bool trackChanges, string[] children);
        void CreateEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }
}
