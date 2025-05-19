using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersionUpdater.Model.JsonConverter
{

    public class VersionJsonConverter : JsonConverter<VersionInfo>
    {
        public override VersionInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                string? version = reader.GetString();

                if (string.IsNullOrWhiteSpace(version)) throw new JsonException("version is null or empty.");

                var versionParts = version.Split('.');

                if (versionParts.Length != 3)
                    throw new JsonException($"Invalid verison format {version}. Expected format: Major:Minor.Patch");

                if (!int.TryParse(versionParts[0], out int major))
                    throw new JsonException($"Invalid major version value: '{versionParts[0]}'");

                if (!int.TryParse(versionParts[1], out int minor))
                    throw new JsonException($"Invalid minor version value: '{versionParts[1]}'");

                if (!int.TryParse(versionParts[2], out int patch))
                    throw new JsonException($"Invalid patch version value: '{versionParts[2]}'");

                return new VersionInfo
                {
                    Major = major,
                    Minor = minor,
                    Patch = patch
                };
            }
            catch (Exception ex)
            {
                throw new JsonException("Failed to parse the Version string.", ex);
            }
        }

        public override void Write(Utf8JsonWriter writer, VersionInfo value, JsonSerializerOptions options)
        {
            string version = $"{value.Major}.{value.Minor}.{value.Patch}";
            writer.WriteStringValue(version);
        }
    }

}
