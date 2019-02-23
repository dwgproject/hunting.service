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
            modelBuilder.Entity<HunterHunting>().HasKey(hh => new { hh.HunterId, hh.HuntingId });
            modelBuilder.Entity<HunterHunting>().HasOne(h => h.Hunter).WithMany(s=>s.Huntings).HasForeignKey(k=>k.HunterId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<HunterHunting>().HasOne(h => h.Hunting).WithMany(s=>s.Hunters).HasForeignKey(k => k.HuntingId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Hunter>().HasBaseType<User>();
            modelBuilder.Entity<User>().HasOne(r => r.Role).WithMany().OnDelete(DeleteBehavior.Restrict);
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