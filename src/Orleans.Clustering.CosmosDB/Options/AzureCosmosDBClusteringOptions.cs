using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Orleans.Clustering.CosmosDB.Options
{
    public class AzureCosmosDBClusteringOptions
    {
        private const string ORLEANS_DB = "Orleans";
        private const string ORLEANS_CLUSTER_COLLECTION = "OrleansCluster";
        private const int ORLEANS_CLUSTER_COLLECTION_THROUGHPUT = 400;

        [JsonProperty(nameof(ClusterId))]
        public string ClusterId { get; set; }

        [JsonProperty(nameof(AccountEndpoint))]
        public string AccountEndpoint { get; set; }

        [JsonProperty(nameof(AccountKey))]
        public string AccountKey { get; set; }

        [JsonProperty(nameof(CanCreateResources))]
        public bool CanCreateResources { get; set; }

        [JsonProperty(nameof(DB))]
        public string DB { get; set; } = ORLEANS_DB;

        [JsonProperty(nameof(Collection))]
        public string Collection { get; set; } = ORLEANS_CLUSTER_COLLECTION;

        [JsonProperty(nameof(CollectionThroughput))]
        public int CollectionThroughput { get; set; } = ORLEANS_CLUSTER_COLLECTION_THROUGHPUT;

        [JsonProperty(nameof(ConnectionMode))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ConnectionMode ConnectionMode { get; set; } = ConnectionMode.Direct;

        [JsonProperty(nameof(ConnectionProtocol))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Protocol ConnectionProtocol { get; set; } = Protocol.Tcp;

        /// <summary>
        /// Automatically add/update stored procudures on initialization.  This may result in slight downtime due to stored procedures having to be deleted and recreated in partitioned environments.
        /// Make sure this is false if you wish to strictly control downtime.
        /// </summary>
        [JsonProperty(nameof(AutoUpdateStoredProcedures))]
        public bool AutoUpdateStoredProcedures { get; set; }

        /// <summary>
        /// Delete the database on initialization.  Useful for testing scenarios.
        /// </summary>
        [JsonProperty(nameof(DropDatabaseOnInit))]
        public bool DropDatabaseOnInit { get; set; }
    }
}
