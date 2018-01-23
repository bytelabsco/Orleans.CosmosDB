using Orleans.CosmosDB.Tests.Grains;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Orleans.CosmosDB.Tests.PersistenceTests;

namespace Orleans.CosmosDB.Tests
{
    public class PersistenceTests : IClassFixture<StorageFixture>
    {
        public const string TEST_STORAGE = "TEST_STORAGE_PROVIDER";
        private StorageFixture _fixture;

        public class StorageFixture : OrleansFixture
        {
            protected override ISiloHostBuilder PreBuild(ISiloHostBuilder builder)
            {
                var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
                siloConfig.AddAzureCosmosDBPersistence(TEST_STORAGE);
                return builder.UseConfiguration(siloConfig)                    
                    .UseAzureCosmosDBPersistence(opt =>
                    {
                        opt.AccountEndpoint = "https://localhost:8081";
                        opt.AccountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
                        opt.ConnectionMode = Microsoft.Azure.Documents.Client.ConnectionMode.Gateway;
                        opt.AutoUpdateSprocs = true;
                    });
            }
        }

        public PersistenceTests(StorageFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task Write_Test()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                var grain = this._fixture.Client.GetGrain<ITestGrain>(i);
                tasks.Add(grain.Write($"Test {i}"));
            }

            await Task.WhenAll(tasks);
            foreach (var t in tasks)
            {
                Assert.True(t.IsCompletedSuccessfully);
            }
        }
    }
}
