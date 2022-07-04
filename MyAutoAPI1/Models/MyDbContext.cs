using Microsoft.EntityFrameworkCore;

namespace MyAutoAPI1.Models
{
    public  class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Statement> Statement { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
