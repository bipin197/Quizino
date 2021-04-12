using Microsoft.EntityFrameworkCore;

namespace Persistence.Test
{
    public class TestPlayerCosmoDbContext: DbContext
    {
        private readonly string _endPoint;
        private readonly string _key;
        private readonly string _dbName;
        public DbSet<Player> Players { get; set; }
        public TestPlayerCosmoDbContext(string endPoint, string key, string dbName)
        {
            _endPoint = endPoint;
            _key = key;
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                _endPoint,
                _key,
                databaseName: _dbName);
    }
}
