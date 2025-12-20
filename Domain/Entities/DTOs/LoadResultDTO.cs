using Newtonsoft.Json;
using System.ComponentModel;

namespace Domain.Entities.DTOs
{
    public class LoadResultDTO<T>
    {
        public IEnumerable<T> data { get; set; }
        [DefaultValue(-1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int totalCount { get; set; } = -1;
        [DefaultValue(-1)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int groupCount { get; set; } = -1;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object[] summary { get; set; }
    }

}
