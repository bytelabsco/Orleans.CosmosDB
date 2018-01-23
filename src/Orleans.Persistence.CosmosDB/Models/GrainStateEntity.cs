using Newtonsoft.Json;

namespace Orleans.Persistence.CosmosDB.Models
{
    internal class GrainStateEntity
    {
        private const string ID_FIELD = "id";
        private const string ETAG_FIELD = "_etag";
        private const string STATE_FIELD = "state";
        private const string GRAIN_TYPE_FIELD = "grainType";

        [JsonProperty(ID_FIELD)]
        public string Id { get; set; }
        
        [JsonProperty(GRAIN_TYPE_FIELD)]
        public string GrainType { get; set; }

        [JsonProperty(STATE_FIELD)]
        public object State { get; set; }

        [JsonProperty(ETAG_FIELD)]
        public string ETag { get; set; }
    }
}
