using Microsoft.EntityFrameworkCore;

namespace IS_server.Data
{
    public class DatabaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Terapija> Terapije { get; set; }
        public DbSet<Sesija> Sesije { get; set; }
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Oprema> Opreme { get; set; }
        public DbSet<Izvestaj> Izvestaji { get; set; }
        public DbSet<Message> Poruke { get; set; }

        public DatabaseContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
