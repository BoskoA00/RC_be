using Microsoft.EntityFrameworkCore;

namespace IS_server.Data
{
    public class DatabaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Therapy> Therapies{ get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DatabaseContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
