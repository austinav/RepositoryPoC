//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RepositoryPoC.AkosNagy.Usual
//{
//    public class CategoryRepository : IRepository<Category>
//    {
//        private readonly DbSet<Category> targetDbSet;
//        private readonly DbContext dbContext;

//        public CategoryRepository(DbContext context)
//        {
//            dbContext = context;
//            targetDbSet = dbContext.Stt<Category>();
//        }

//        public void Add(Category entity)
//        {
//            targetDbSet.Add(entity);
//        }

//        public void Delete(Category entity)
//        {
//            var entry = dbContext.Entry(entity);
//            if (entry == null || entry.State == EntityState.Detached)
//            {
//                targetDbSet.Attach(entity);
//            }
//            entry.State = EntityState.Deleted;
//        }

//        public void Update(Category entity)
//        {
//            var entry = dbContext.Entry(entity);
//            if (entry == null || entry.State == EntityState.Detached)
//            {
//                targetDbSet.Attach(entity);
//            }
//            entry.State = EntityState.Modified;
//        }

//        public IQueryable<Category> List()
//        {
//            return targetDbSet;
//        }
//    }
//}
