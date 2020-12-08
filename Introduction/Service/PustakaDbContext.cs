
using Introduction.Entity;
using Microsoft.EntityFrameworkCore;

namespace Introduction.Service
{
    public class PustakaDbContext : DbContext
    {
        public PustakaDbContext(DbContextOptions<PustakaDbContext>
            options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}
