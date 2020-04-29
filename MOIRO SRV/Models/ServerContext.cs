using System.Data.Entity;

namespace MOIRO_SRV.Models
{
    public class ServerContext : DbContext
    {
        static ServerContext()
        {
            Database.SetInitializer(new AppDbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<PublicChat> PublicChats { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<ServerUser> ServerUsers { get; set; }
        public DbSet<Webinar> Webinars { get; set; }
        public DbSet<WebinarSettings> WebinarsSettings { get; set; }
        public DbSet<Platform> Platforms { get; set; }
    }
}