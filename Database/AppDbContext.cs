using Estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companys { get; set; }
        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<Movements> Movements { get; set; }
        public DbSet<ReportsDaily> ReportsDaily { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString =
                "Server=localhost;Database=estacionamento;User=jpdev;Password=12345678;";
            optionsBuilder
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}