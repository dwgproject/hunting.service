using Microsoft.EntityFrameworkCore;
using Hunt.Model;

namespace Hunt.Data
{
    public class HuntContext : DbContext{
        public HuntContext(DbContextOptions<HuntContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }
        

    }
}