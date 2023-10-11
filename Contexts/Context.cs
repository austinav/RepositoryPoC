using Microsoft.EntityFrameworkCore;

using RepositoryPoC.Entities;

namespace RepositoryPoC.Contexts
{
    public class RepositoryPoCContext : DbContext
    {
        public RepositoryPoCContext()
            : base("Data Source=localhost\\SqlServer;Initial Catalog=RepositoryPoC;Integrated Security=True")
        {
        }

        public virtual DbSet<Entity1> Entity1 { get; set; }
        public virtual DbSet<Entity2> Entity2 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity2>()
                .HasMany<Entity1>(e => e.Entity1s)
                .WithOptional(e => e.Entity2)
                .HasForeignKey(e => e.Entity2_Id);
        }
    }
}
