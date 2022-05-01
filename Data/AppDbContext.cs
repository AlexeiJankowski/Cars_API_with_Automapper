using Microsoft.EntityFrameworkCore;

namespace BMW_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Car> Cars {get;set;}
    }
}