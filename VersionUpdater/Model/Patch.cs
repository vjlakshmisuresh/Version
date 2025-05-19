using System.Text.Json.Serialization;

namespace VersionUpdater.Model
{
    public class Patch
    {
        [JsonPropertyName("Name")]
        public String? Name { get; set; }

        [JsonPropertyName("Directory")]
        public String? Directory { get; set; }

        [JsonPropertyName("Ordinal")]
        public String? Ordinal { get; set; }

        [JsonPropertyName("Scripts")]
        public List<String>? Scripts { get; set; }


    }
}
