using OnlineStore.Domain.Entities;
using System.Data.Entity;

namespace OnlineStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }

        public EFDbContext(string connectionString) : base(nameOrConnectionString: connectionString)
        { }
    }
}