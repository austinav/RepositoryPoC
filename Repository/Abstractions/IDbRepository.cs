namespace RepositoryPoC.Repository.Abstractions
{
    /// <summary>
    /// DbRepositories will need to include the data access entity that is used by a ORM/provider in order to provide the 
    /// IRepository functions for primary domain data objects.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="Tdto"></typeparam>
    internal interface IDbRepository<TEntity, Tdto> : IRepository<Tdto> where TEntity : class where Tdto : class
    {
    }
}