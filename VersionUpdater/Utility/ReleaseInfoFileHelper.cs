using System.Text.Json;
using System.Text.Json.Serialization;
using VersionUpdater.Model;

namespace VersionUpdater.Utility
{
    public class ReleaseInfoFileHelper
    {
        public static ReleaseInfo? ReadFromFile(string path)
        {
            

            string releaseInfo = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(releaseInfo))
            {
                throw new JsonException("File content is null or empty.");
            }

            ReleaseInfo? relInfo;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,

            };

            return JsonSerializer.Deserialize<ReleaseInfo>(releaseInfo, options);

        }

        public static void WriteToFile(string path, ReleaseInfo releaseInfo)
        {
            if (path == null || releaseInfo == null)
            {
                throw new ArgumentNullException("File path or releaseInfo is null.");
            }

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };

            string releaseInfostr = JsonSerializer.Serialize(releaseInfo, options);

            File.WriteAllText(path, releaseInfostr);
        }
    }
}
