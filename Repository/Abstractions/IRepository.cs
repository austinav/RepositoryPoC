using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryPoC.Repository.Abstractions
{
    /// <summary>
    /// Defines the contract for All Repositories at the consistent abstract high-level. It should not matter what the data source is: database, web api, json, etc. The 
    /// implementation of this interface will differ based on source, but interacting with data should be the same and be based on normalized object model that 
    /// makes sense for the actual semantic context.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Tdto"></typeparam>
    public interface IRepository<Tdto> : IEnumerable<Tdto>, IQueryable<Tdto>
    {
        void Delete(object id);
        void Delete(Tdto entityToDelete);
        IEnumerable<Tdto> Get(Expression<Func<Tdto, bool>> filter = null, Func<IQueryable<Tdto>, IOrderedQueryable<Tdto>> orderBy = null, string includeProperties = "");
        //Tdto GetByID(object id);
        void Insert(Tdto entity);
        void Update(Tdto entityToUpdate);
    }
}