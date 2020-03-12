using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MOIRO_SRV.Models
{
    public class ServerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<PublicChat> PublicChats { get; set; }

        //public DbSet<User> Users { get; set; }
    }
}