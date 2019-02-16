using Microsoft.EntityFrameworkCore;
using Hunt.Model;

namespace Hunt.Data
{
    public class HuntContext : DbContext{

        public HuntContext(DbContextOptions<HuntContext> options):base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Hunting>().HasOne(i=>i.Leader).WithOne();
        } 

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Hunting> Huntings {get;set;}

        public DbSet<Score> Scores {get;set;}
        public DbSet<Hunter> Hunters {get;set;}
    }
}