using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace RepositoryPoC.Repository
{
    internal interface IRepoDataSet<TEntity> where TEntity : class
    {
        ObservableCollection<TEntity> Local { get; }

        TEntity Add(TEntity entity);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        TEntity Attach(TEntity entity);

        TEntity Create();

        TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;

        bool Equals(object obj);

        TEntity Find(params object[] keyValues);

        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);

        int GetHashCode();

        Type GetType();

        TEntity Remove(TEntity entity);

        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters);

        //should this be a custom repo behavior?
        //static implicit operator DbSet(DbSet<TEntity> entry);
    }
}
