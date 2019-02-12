using Microsoft.EntityFrameworkCore;
using Hunt.Model;

namespace Hunt.Data
{
    public class HuntContext : DbContext{

        private string password = "";
        private string user = "";
        public HuntContext(DbContextOptions<HuntContext> options):base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString=$"Data Source=mssql6.webio.pl,2401;Initial Catalog=patrykapriasz_jaegerPrime;User ID={user};Password={password}";
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=hunting;Trusted_Connection=True;MultipleActiveResultSets=true");            
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(i=>i.Role).WithMany().OnDelete(DeleteBehavior.Restrict);
        } 

        public DbSet<User> Users { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Hunting> Huntings {get;set;}

        public DbSet<Score> Scores {get;set;}
    }
}