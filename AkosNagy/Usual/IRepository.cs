using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPoC.AkosNagy.Usual
{
    /* Source article from Akos Nagy
     * https://dotnetfalcon.com/implementing-the-repository-pattern-with-direct-iqueryable-support/
     */
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IQueryable<T> List();
    }
}
