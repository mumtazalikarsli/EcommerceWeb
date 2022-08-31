using DataAccess.Concrete.EntityFramework.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Concrete.Contexts
{
    public class ECommerceProjectWithWebAPIContext : DbContext
    {
        public ECommerceProjectWithWebAPIContext(DbContextOptions<ECommerceProjectWithWebAPIContext> options) : base(options) { }
        public ECommerceProjectWithWebAPIContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = "Data Source =DMGM0349112; Initial Catalog = NewBase2;user id=sa;password=765543275Mm";
            optionsBuilder.UseSqlServer(connString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
