using Microsoft.EntityFrameworkCore;
using Hunt.Model;

namespace Hunt.Data
{
    public class HuntContext : DbContext{
        public HuntContext() 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString="";
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=hunting;Trusted_Connection=True;MultipleActiveResultSets=true");            
        }   

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Hunting> Huntings {get;set;}

        public DbSet<Score> Scores {get;set;}
    }
}