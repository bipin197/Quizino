using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Test
{
    public class TestPlayerCosmoDbContextTest
    {
        private string _endPoint;
        private string _key;
        private string _dbName;
        [SetUp]
        public void Setup()
        {
            _endPoint = "";
            _key = "";
            _dbName = "Test-Db";
        }

        [Test]
        public async Task TestCreateDeleteEntity()
        {       
            using (var dbContext = new TestPlayerCosmoDbContext(_endPoint, _key, _dbName))
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();

                var player = new Player()
                {
                    Id = 1,
                    Name = "John Doe",
                    Age = 23,
                    Country = "France",
                    Position = "Forward"
                };
                dbContext.Add(player);
                await dbContext.SaveChangesAsync();

                var foundPlayer = await dbContext.Players.FindAsync(1);
                await ClearDataAsync(dbContext, new[] { player });
                Assert.IsNotNull(foundPlayer);
            }
        }

        [Test]
        public async Task TestUpdateEntity()
        {
            using (var dbContext = new TestPlayerCosmoDbContext(_endPoint, _key, _dbName))
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();

                var player = new Player()
                {
                    Id = 1,
                    Name = "John Doe",
                    Age = 23,
                    Country = "France",
                    Position = "Forward"
                };
                dbContext.Add(player);
                await dbContext.SaveChangesAsync();

                player.Age = 25;
                dbContext.Players.Update(player);
                await dbContext.SaveChangesAsync();
                var foundPlayer = await dbContext.Players.FindAsync(1);

                await ClearDataAsync(dbContext, new[] { player });
                Assert.AreEqual(foundPlayer.Age, 25);
            }
        }

        [Test]
        public async Task TestMultiCreateDeleteEntity()
        {
            using (var dbContext = new TestPlayerCosmoDbContext(_endPoint, _key, _dbName))
            {
                var players = await InitMultiPlayersAsync(dbContext);

                await dbContext.AddRangeAsync(players);
                await dbContext.SaveChangesAsync();

                // Find player at any random index
                var foundPlayer = await dbContext.Players.FindAsync(new Random().Next(1, 10));
                await ClearDataAsync(dbContext, players);
                Assert.IsNotNull(foundPlayer);
            }
        }

        [Test]
        public async Task TestMultiUpdateEntity()
        {
            using (var dbContext = new TestPlayerCosmoDbContext(_endPoint, _key, _dbName))
            {
                var players = await InitMultiPlayersAsync(dbContext);

                await dbContext.AddRangeAsync(players);
                await dbContext.SaveChangesAsync();
                foreach (var player in players)
                {
                    player.Age = 25;
                }

                dbContext.Players.UpdateRange(players);
                await dbContext.SaveChangesAsync();

                // Find player at any random index
                var foundPlayer = await dbContext.Players.FindAsync(new Random().Next(1, 10));
                await ClearDataAsync(dbContext, players);
                Assert.AreEqual(foundPlayer.Age, 25);
            }
        }

        private static async Task<List<Player>> InitMultiPlayersAsync(TestPlayerCosmoDbContext dbContext)
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var players = new List<Player>();
            for (int id = 1; id < 11; id++)
            {
                var player = new Player()
                {
                    Id = id,
                    Name = "John Doe" + id,
                    Age = 23,
                    Country = "France",
                    Position = "Forward"
                };

                players.Add(player);
            }

            return players;
        }

        private async Task ClearDataAsync(TestPlayerCosmoDbContext dbContext, IEnumerable<Player> players)
        {
            foreach(var player in players)
            {
                dbContext.Players.Remove(player);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}