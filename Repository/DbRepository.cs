using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using RepositoryPoC.Repository.Abstractions;

using System.Collections;
using System.Linq.Expressions;

namespace RepositoryPoC.EmailService.Repo
{
    /// <summary>
    /// Repository<<typeparamref name="TEntity"/>,<typeparamref name="Tdto"/>> uses Generics to implement standard Repository usage to any Entity with transparency to the advantages of 
    /// Object-Relational Mapper, specifically MS Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Tdto"></typeparam>
    public class DbRepository<TEntity, Tdto> : IDbRepository<TEntity, Tdto> where TEntity : class where Tdto : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        
        readonly static string _cacheKeyAll = $"{nameof(TEntity)}::{""}";
        readonly static string _cacheKeyUser = $"{nameof(TEntity)}::{""}::{{0}}";

        public DbRepository(DbContext context, IMemoryCache cache, IMapper mapper)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();

            _cache = cache;
            _mapper = mapper;
        }

        //We really don't even want this method because we should be using the base IQueryable extensions with standard Linq lambdas
        //btw, this compiles, but that don't mean it works
        public virtual IEnumerable<Tdto> Get(
            Expression<Func<Tdto, bool>> filter = null,
            Func<IQueryable<Tdto>, IOrderedQueryable<Tdto>> orderBy = null,
            string includeProperties = "")
        {
            //https://docs.automapper.org/en/stable/Expression-Translation-(UseAsDataSource).html
            //we need the expression translation here
            IQueryable<Tdto> query = this.AsQueryable<Tdto>();
            IEnumerable<Tdto> result;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                result = orderBy(query).ToList();
            }
            else
            {
                result = query.ToList();
            }

            _cache.Set(string.Format(_cacheKeyUser, result.GetHashCode()), result);

            return result;
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(Tdto dto)
        {
            dbSet.Add(_mapper.Map<TEntity>(dto));
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Tdto objectToDelete)
        {
            if (context.Entry(objectToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(_mapper.Map<TEntity>(objectToDelete));
            }
            dbSet.Remove(_mapper.Map<TEntity>(objectToDelete));
        }

        public virtual void Update(Tdto objectToUpdate)
        {
            dbSet.Attach(_mapper.Map<TEntity>(objectToUpdate));
            context.Entry(_mapper.Map<TEntity>(objectToUpdate)).State = EntityState.Modified;
        }

        public IEnumerator<Tdto> GetEnumerator()
        {
            return _mapper.Map<IEnumerable<Tdto>>(dbSet.AsEnumerable()).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _mapper.Map<IEnumerable<Tdto>>(dbSet.AsEnumerable()).GetEnumerator();
        }

        public Expression Expression => throw new NotImplementedException();

        public Type ElementType => throw new NotImplementedException();

        // This is the hard part. We need this translate the IQueryable decorator chain for the TDto into expressions for TEntity, 
        // in order for that expression chain to be provided to the sql provider for translation into SQL via the ORM.
        public IQueryProvider Provider
        {
            get => dbSet.AsQueryable<TEntity>().Provider;
        }
    }
}
