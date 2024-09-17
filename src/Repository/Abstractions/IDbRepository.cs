namespace RepositoryPoC.Repository.Abstractions
{
    /* The problem with the Generic Repo is that I have to determine my type slots in the repo design.
     * I cannot choose to implement using to Entities, or three to 1 Dto/Domain data object. 
     * This is a major problem for providing ideal flexibility in mapping the data access code,
     *  which is fairly coupled to the persistence technology, and my domain which may be completely different.
     * It seems best case scenario is settling for 1-to-1 Dto-Entity mapping, but then we require another layer of mapping,
     *  which was what I was trying to avoid, because we're back to coding a repo layer every time or "why not just use the entities".
     *      
     */
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