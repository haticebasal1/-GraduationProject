using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PhoneCase.MVC.ViewModels
{
    public class ResponseViewModel<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; } = default!;

        [JsonProperty("isSuccessful")]
        public bool IsSuccessful { get; set; }

        private JToken _errors;

        [JsonProperty("errors")]
        private JToken ErrorsInternal
        {
            set
            {
                _errors = value;
                Errors = new List<string>();

                if (value == null) return;

                if (value.Type == JTokenType.Array)
                {
                    Errors = value.ToObject<List<string>>()!;
                }
                else if (value.Type == JTokenType.Object)
                {
                    foreach (var prop in ((JObject)value).Properties())
                    {
                        Errors.Add(prop.Value.ToString());
                    }
                }
                else
                {
                    Errors.Add(value.ToString());
                }
            }
        }

        [JsonIgnore]
        public List<string> Errors { get; set; } = new();

        [JsonIgnore]
        public int StatusCode { get; set; }
    }
}
