using Microsoft.EntityFrameworkCore;
using Hunt.Model;

namespace Hunt.Data
{

    public class HuntContext : DbContext{
        public HuntContext(DbContextOptions<HuntContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}