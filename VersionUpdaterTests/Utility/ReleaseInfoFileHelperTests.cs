using VersionUpdater.Model;
using VersionUpdater.Utility;

namespace VersionUpdaterTests.Utility
{
    [TestFixture]
    public class ReleaseInfoFileHelperTests
    {
        private string _tempFilePath;

        [SetUp]
        public void Setup()
        {
            _tempFilePath = Path.GetTempFileName();
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        [Test]
        public void ReleaseInfoFileHelper_ReadFromFile_ShouldReturnReleaseInfo_WhenValidJsonProvided()
        {
            var content = """
            {
                "version": "1.2.3",
                "patch": {
                    "name": "PatchA",
                    "directory": "dir",
                    "ordinal": "001",
                    "scripts": ["file1.sql"]
                }
            }
            """;

            File.WriteAllText(_tempFilePath, content);

            var result = ReleaseInfoFileHelper.ReadFromFile(_tempFilePath);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Version.Major, Is.EqualTo(1));
                Assert.That(result.Version.Minor, Is.EqualTo(2));
                Assert.That(result.Version.Patch, Is.EqualTo(3));
            });
        }

        [Test]
        public void ReleaseInfoFileHelper_ReadFromFile_ShouldThrow_WhenPathIsNullOrWhitespace()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _ = ReleaseInfoFileHelper.ReadFromFile("  ");
            });
        }

        [Test]
        public void ReleaseInfoFileHelper_ReadFromFile_ShouldThrow_WhenFileIsEmpty()
        {
            File.WriteAllText(_tempFilePath, "");

            Assert.Throws<System.Text.Json.JsonException>(() =>
            {
                _ = ReleaseInfoFileHelper.ReadFromFile(_tempFilePath);
            });
        }

        [Test]
        public void ReleaseInfoFileHelper_ReadFromFile_ShouldSerializeAndWriteCorrectly()
        {
            var original = new ReleaseInfo
            {
                Version = new VersionInfo { Major = 2, Minor = 1, Patch = 9 },
                Patch = new Patch
                {
                    Name = "Sample",
                    Directory = "scripts",
                    Ordinal = "005",
                    Scripts = new List<string> { "script1", "script2" }
                }
            };

            ReleaseInfoFileHelper.WriteToFile(_tempFilePath, original);
            var result = ReleaseInfoFileHelper.ReadFromFile(_tempFilePath);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Version.Minor, Is.EqualTo(1));
                Assert.That(result.Patch?.Directory, Is.EqualTo("scripts"));
            });
        }

        [Test]
        public void ReleaseInfoFileHelper_WriteToFile_ShouldThrow_WhenPathIsNull()
        {
            var releaseInfo = new ReleaseInfo
            {
                Version = new VersionInfo { Major = 1, Minor = 0, Patch = 0 }
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                ReleaseInfoFileHelper.WriteToFile(null!, releaseInfo);
            });
        }


        [Test]
        public void ReleaseInfoFileHelper_WriteToFile_ShouldThrow_WhenReleaseInfoIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ReleaseInfoFileHelper.WriteToFile(_tempFilePath, null!);
            });
        }
    }
}
