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


        public void CreateEntity(T entity) => _context.Set<T>().Add(entity);

        public void DeleteEntity(T entity) => _context.Set<T>().Remove(entity);

        public void UpdateEntity(T entity) => _context.Set<T>().Update(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges) =>
            trackChanges ? _context.Set<T>().Where(condition) : _context.Set<T>().Where(condition).AsNoTracking(); 

        public IQueryable<T> GetEntityEager(Expression<Func<T, bool>> condition, bool trackChanges, string[] children)
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
