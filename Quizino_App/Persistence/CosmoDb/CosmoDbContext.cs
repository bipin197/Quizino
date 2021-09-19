using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.CosmoDb
{
    public class CosmoDbContext<T> : DbContext where T : class
    {
        private readonly DbConnectionBundle _dbConnectionBundle;
        public DbSet<T> Items { get; set; }
        public CosmoDbContext(DbConnectionBundle dbConnectionBundle)
        {
            _dbConnectionBundle = dbConnectionBundle;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().ToContainer(typeof(T).Name);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                _dbConnectionBundle.EndpointConnection.Endpoint,
                _dbConnectionBundle.EndpointConnection.Key,
                databaseName: _dbConnectionBundle.DatabaseId, 
                options => 
                {
                    
                });

    }
}
