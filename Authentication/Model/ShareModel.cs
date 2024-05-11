using Newtonsoft.Json;

namespace Authentication.Model
{
    public class ShareModel
    {
        public class RemoveModel
        {
            public int Id { get; set; }
            public string Note { get; set; }
        }
        public class CanRemoveModel
        {
            public bool Can { get; set; }
            public string Message { get; set; }
        }

        public class SearchResult<TResult, TSearch>
        {
            [JsonProperty("results")]
            public List<TResult> Results { get; set; }
            [JsonProperty("param")]
            public TSearch Param { get; set; }
        }
    }
}
