using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryPoC.Contexts;
using RepositoryPoC.Entities;

namespace RepositoryPoC.Repository
{
    public class UnitOfWork : IDisposable
    {
        private RepositoryPoCContext context = new RepositoryPoCContext();
        private GenericRepository<Entity1> entity1Repository;
        private GenericRepository<Entity2> entity2Repository;

        public GenericRepository<Entity1> Entity1Repository
        {
            get
            {

                if (entity1Repository == null)
                {
                    entity1Repository = new GenericRepository<Entity1>(context);
                }
                return entity1Repository;
            }
        }

        public GenericRepository<Entity2> Entity2Repository
        {
            get
            {

                if (entity2Repository == null)
                {
                    entity2Repository = new GenericRepository<Entity2>(context);
                }
                return entity2Repository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
