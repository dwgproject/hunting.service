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
            modelBuilder.Entity<UserHunting>().HasKey(hh => new { hh.UserId, hh.HuntingId });
            modelBuilder.Entity<UserHunting>().HasOne(h => h.User).WithMany(s=>s.Huntings).HasForeignKey(k=>k.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserHunting>().HasOne(h => h.Hunting).WithMany(s=>s.Users).HasForeignKey(k => k.HuntingId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasOne(r => r.Role).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Hunting>().HasMany(x=>x.Quarries).WithOne().OnDelete(DeleteBehavior.Restrict);
        } 

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Hunting> Huntings {get;set;}

        public DbSet<Score> Scores {get;set;}
        public DbSet<Quarry> Quarries { get; set; }
    }
}