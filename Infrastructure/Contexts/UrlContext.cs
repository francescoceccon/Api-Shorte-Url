using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorys
{
    public class UrlContext : DbContext
    {
        public DbSet<MottuUrl> Url { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=172.17.0.2;Port=5432;Database=postgres;Username=root;Password=root;";
            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
