using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.DB
{
    public class ZAPNETApplicationContext : DbContext
    {

        public IConfiguration Configuration { get; }

        public ZAPNETApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().HasKey(e => e.Id);
        }
    }
}
