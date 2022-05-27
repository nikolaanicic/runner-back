using Contracts.Repository;
using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Entities.Repository
{
    /// <summary>
    /// This class is the base of all repository implementations
    /// Idea is that a concrete repository should be sort of an adapter between its interface and this class
    /// Where this class provides the implementation needed and the interface provides application needed interface
    /// example:
    /// 
    /// ProductRepository: RepositoryBase<Product>, IProductRepository
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class RepositoryBase<T> : IRepostoryBase<T>
        where T: class
    {

        private DatabaseContext _context;

        public RepositoryBase(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method for creating the entity of type T in the context
        /// </summary>
        /// <param name="entity"></param>
        public void CreateEntity(T entity) => _context.Set<T>().Add(entity);


        /// <summary>
        /// Method for deleting an entity of type T from the context
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteEntity(T entity) => _context.Set<T>().Remove(entity);


        /// <summary>
        /// Method for updating an entity of type T in the context
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateEntity(T entity) => _context.Set<T>().Update(entity);


        /// <summary>
        /// Method that retrieves all of the entities of type T from the context
        /// </summary>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();


        /// <summary>
        /// Method that filters the entites in the context based on a condition
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trackChanges"></param>
        /// <returns></returns>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges) =>
            trackChanges ? _context.Set<T>().Where(condition) : _context.Set<T>().Where(condition).AsNoTracking(); 


        /// <summary>
        /// Method that filters entites based on a condition and attaches children entites 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="trackChanges"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntitiesEager(Expression<Func<T, bool>> condition, bool trackChanges, string[] children)
        {
            var query = trackChanges ? _context.Set<T>().Where(condition) : _context.Set<T>().Where(condition).AsNoTracking();

            foreach(string child in children)
            {
                query = query.Include(child);
            }
         
            return query;
        }
    }
}
