//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Collections;

//namespace RepositoryPoC.Data.Repo
//{
//    internal class RepoDataSetEFAdapter<TEntity> : IRepoDataSet<TEntity>, IDbSet<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable where TEntity : class
//    {
//        private DbSet<TEntity> dbSet;

//        public ObservableCollection<TEntity> Local { get; }

//        TEntity Add(TEntity entity)
//        {
//            return entity; 
//        }

//        //
//        // Summary:
//        //     Adds the given collection of entities into context underlying the set with each
//        //     entity being put into the Added state such that it will be inserted into the
//        //     database when SaveChanges is called.
//        //
//        // Parameters:
//        //   entities:
//        //     The collection of entities to add.
//        //
//        // Returns:
//        //     The collection of entities.
//        //
//        // Remarks:
//        //     Note that if System.Data.Entity.Infrastructure.DbContextConfiguration.AutoDetectChangesEnabled
//        //     is set to true (which is the default), then DetectChanges will be called once
//        //     before adding any entities and will not be called again. This means that in some
//        //     situations AddRange may perform significantly better than calling Add multiple
//        //     times would do. Note that entities that are already in the context in some other
//        //     state will have their state set to Added. AddRange is a no-op for entities that
//        //     are already in the context in the Added state.
//        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
//        public TEntity Attach(TEntity entity);
//        public TEntity Create();
//        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public override bool Equals(object obj);
//        //
//        // Summary:
//        //     Finds an entity with the given primary key values. If an entity with the given
//        //     primary key values exists in the context, then it is returned immediately without
//        //     making a request to the store. Otherwise, a request is made to the store for
//        //     an entity with the given primary key values and this entity, if found, is attached
//        //     to the context and returned. If no entity is found in the context or the store,
//        //     then null is returned.
//        //
//        // Parameters:
//        //   keyValues:
//        //     The values of the primary key for the entity to be found.
//        //
//        // Returns:
//        //     The entity found, or null.
//        //
//        // Exceptions:
//        //   T:System.InvalidOperationException:
//        //     Thrown if multiple entities exist in the context with the primary key values
//        //     given.
//        //
//        //   T:System.InvalidOperationException:
//        //     Thrown if the type of entity is not part of the data model for this context.
//        //
//        //   T:System.InvalidOperationException:
//        //     Thrown if the types of the key values do not match the types of the key values
//        //     for the entity type to be found.
//        //
//        //   T:System.InvalidOperationException:
//        //     Thrown if the context has been disposed.
//        //
//        // Remarks:
//        //     The ordering of composite key values is as defined in the EDM, which is in turn
//        //     as defined in the designer, by the Code First fluent API, or by the DataMember
//        //     attribute.
//        public virtual TEntity Find(params object[] keyValues);
//        //
//        // Summary:
//        //     Asynchronously finds an entity with the given primary key values. If an entity
//        //     with the given primary key values exists in the context, then it is returned
//        //     immediately without making a request to the store. Otherwise, a request is made
//        //     to the store for an entity with the given primary key values and this entity,
//        //     if found, is attached to the context and returned. If no entity is found in the
//        //     context or the store, then null is returned.
//        //
//        // Parameters:
//        //   cancellationToken:
//        //     A System.Threading.CancellationToken to observe while waiting for the task to
//        //     complete.
//        //
//        //   keyValues:
//        //     The values of the primary key for the entity to be found.
//        //
//        // Returns:
//        //     A task that represents the asynchronous find operation. The task result contains
//        //     the entity found, or null.
//        //
//        // Exceptions:
//        //   T:System.InvalidOperationException:
//        //     Thrown if multiple entities exist in the context with the primary key values
//        //     given.
//        //
//        //   T:System.InvalidOperationException:
//        //     Thrown if the type of entity is not part of the data model for this context.
//        //
//        //   T:System.InvalidOperationException:
//        //     Thrown if the types of the key values do not match the types of the key values
//        //     for the entity type to be found.
//        //
//        //   T:System.InvalidOperationException:
//        //     Thrown if the context has been disposed.
//        //
//        // Remarks:
//        //     The ordering of composite key values is as defined in the EDM, which is in turn
//        //     as defined in the designer, by the Code First fluent API, or by the DataMember
//        //     attribute. Multiple active operations on the same context instance are not supported.
//        //     Use 'await' to ensure that any asynchronous operations have completed before
//        //     calling another method on this context.
//        public virtual Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
//        //
//        // Summary:
//        //     Asynchronously finds an entity with the given primary key values. If an entity
//        //     with the given primary key values exists in the context, then it is returned
//        //     immediately without making a request to the store. Otherwise, a request is made
//        //     to the store for an entity with the given primary key values and this entity,
//        //     if found, is attached to the context and returned. If no entity is found in the
//        //     context or the store, then null is returned.
//        //
//        // Parameters:
//        //   keyValues:
//        //     The values of the primary key for the entity to be found.
//        //
//        // Returns:
//        //     A task that represents the asynchronous find operation. The task result contains
//        //     the entity found, or null.
//        //
//        // Remarks:
//        //     The ordering of composite key values is as defined in the EDM, which is in turn
//        //     as defined in the designer, by the Code First fluent API, or by the DataMember
//        //     attribute. Multiple active operations on the same context instance are not supported.
//        //     Use 'await' to ensure that any asynchronous operations have completed before
//        //     calling another method on this context.
//        public virtual Task<TEntity> FindAsync(params object[] keyValues);
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public override int GetHashCode();
//        [EditorBrowsable(EditorBrowsableState.Never)]
//        public Type GetType();
//        public virtual TEntity Remove(TEntity entity);
//        //
//        // Summary:
//        //     Removes the given collection of entities from the context underlying the set
//        //     with each entity being put into the Deleted state such that it will be deleted
//        //     from the database when SaveChanges is called.
//        //
//        // Parameters:
//        //   entities:
//        //     The collection of entities to delete.
//        //
//        // Returns:
//        //     The collection of entities.
//        //
//        // Remarks:
//        //     Note that if System.Data.Entity.Infrastructure.DbContextConfiguration.AutoDetectChangesEnabled
//        //     is set to true (which is the default), then DetectChanges will be called once
//        //     before delete any entities and will not be called again. This means that in some
//        //     situations RemoveRange may perform significantly better than calling Remove multiple
//        //     times would do. Note that if any entity exists in the context in the Added state,
//        //     then this method will cause it to be detached from the context. This is because
//        //     an Added entity is assumed not to exist in the database such that trying to delete
//        //     it does not make sense.
//        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
//        //
//        // Summary:
//        //     Creates a raw SQL query that will return entities in this set. By default, the
//        //     entities returned are tracked by the context; this can be changed by calling
//        //     AsNoTracking on the System.Data.Entity.Infrastructure.DbSqlQuery`1 returned.
//        //     Note that the entities returned are always of the type for this set and never
//        //     of a derived type. If the table or tables queried may contain data for other
//        //     entity types, then the SQL query must be written appropriately to ensure that
//        //     only entities of the correct type are returned. As with any API that accepts
//        //     SQL it is important to parameterize any user input to protect against a SQL injection
//        //     attack. You can include parameter place holders in the SQL query string and then
//        //     supply parameter values as additional arguments. Any parameter values you supply
//        //     will automatically be converted to a DbParameter. context.Blogs.SqlQuery("SELECT
//        //     * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor); Alternatively, you
//        //     can also construct a DbParameter and supply it to SqlQuery. This allows you to
//        //     use named parameters in the SQL query string. context.Blogs.SqlQuery("SELECT
//        //     * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
//        //
//        // Parameters:
//        //   sql:
//        //     The SQL query string.
//        //
//        //   parameters:
//        //     The parameters to apply to the SQL query string. If output parameters are used,
//        //     their values will not be available until the results have been read completely.
//        //     This is due to the underlying behavior of DbDataReader, see http://go.microsoft.com/fwlink/?LinkID=398589
//        //     for more details.
//        //
//        // Returns:
//        //     A System.Data.Entity.Infrastructure.DbSqlQuery`1 object that will execute the
//        //     query when it is enumerated.
//        public virtual DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters);

//        //
//        // Summary:
//        //     Returns the equivalent non-generic System.Data.Entity.DbSet object.
//        //
//        // Parameters:
//        //   entry:
//        //     The generic set object.
//        //
//        // Returns:
//        //     The non-generic set object.
//        //public static implicit operator DbSet(DbSet<TEntity> entry);
//    }
//}

