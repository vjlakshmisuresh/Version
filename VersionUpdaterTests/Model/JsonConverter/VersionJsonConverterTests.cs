using System.Text.Json;
using VersionUpdater.Model;
using VersionUpdater.Model.JsonConverter;

namespace VersionUpdaterTests.Model.jsonconverter
{
    [TestFixture]
    public class VersionJsonConverterTests
    {
        private JsonSerializerOptions _options;

        [SetUp]
        public void Setup()
        {
            _options = new JsonSerializerOptions
            {
                Converters = { new VersionJsonConverter() }
            };
        }

        [Test]
        public void VersionJsonConverter_ShouldProduceCorrectJsonString()
        {
            var version = new VersionInfo { Major = 1, Minor = 2, Patch = 3 };
            var json = JsonSerializer.Serialize(version, _options);
            Assert.That(json, Is.EqualTo("\"1.2.3\""));
        }

        [Test]
        public void VersionJsonConverter_ShouldReturnVersionInfo()
        {
            var json = "\"4.5.6\"";
            var version = JsonSerializer.Deserialize<VersionInfo>(json, _options);

            Assert.That(version, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(4));
                Assert.That(version.Minor, Is.EqualTo(5));
                Assert.That(version.Patch, Is.EqualTo(6));
            });
        }

        [Test]
        public void VersionJsonConverter_Deserialize_NullVersionString_ShouldReturnNull()
        {
            var json = "null";
            var version = JsonSerializer.Deserialize<VersionInfo>(json, _options);
            Assert.That(version, Is.Null);
        }

        [TestCase("\"1.2\"", "Failed to parse the Version string.", TestName = "TooFewParts")]
        [TestCase("\"1.2.3.4\"", "Failed to parse the Version string.", TestName = "TooManyParts")]
        [TestCase("\"1.two.3\"", "Failed to parse the Version string.", TestName = "NonNumericMinor")]
        [TestCase("\"x.1.2\"", "Failed to parse the Version string.", TestName = "NonNumericMajor")]
        [TestCase("\"1.2.z\"", "Failed to parse the Version string.", TestName = "NonNumericPatch")]
        public void Deserialize_InvalidVersionFormat_ShouldThrowJsonException(string json, string expectedMessagePart)
        {
            var ex = Assert.Throws<JsonException>(() =>
                JsonSerializer.Deserialize<VersionInfo>(json, _options)
            );

            Assert.That(ex.Message, Does.Contain(expectedMessagePart));
        }

        [Test]
        public void VersionJsonConverter_Deserialize_NullString_ShouldReturnNull()
        {
            var json = "null";
            var version = JsonSerializer.Deserialize<VersionInfo>(json, _options);
            Assert.That(version, Is.Null);
        }
    }
}
