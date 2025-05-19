using System.Text.Json.Serialization;
using VersionUpdater.Model.JsonConverter;

namespace VersionUpdater.Model
{
    public class ReleaseInfo
    {
        [JsonConverter(typeof(VersionJsonConverter))]
        [JsonPropertyName("Version")]
        public required VersionInfo Version { get; set; }

        [JsonPropertyName("Patch")]
        public Patch? Patch { get; set; }

        public VersionInfo GetVersion()
        {
            return Version;
        }
        public void SetVersion(VersionInfo value)
        {
            Version = value;
        }

        public Patch? GetPatch()
        {
            return Patch;
        }

        public void SetPatch(Patch value)
        {
            Patch = value;
        }
    }
}
