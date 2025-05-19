using VersionUpdater.Interfaces;
using VersionUpdater.Model;
using VersionUpdater.Strategy;

namespace VersionUpdaterTests.Strategy
{
    [TestFixture]
    public class PatchVersionUpgradeStrategyTests
    {
        private IVersionUpgradeStrategy _strategy = new PatchVersionUpgradeStrategy();

        [Test]
        public void PatchVersionUpgradeStrategy_ShouldUpgradePatch()
        {
            var version = new VersionInfo { Major = 2, Minor = 3, Patch = 9 };

            _strategy.UpgradeVersion(version);

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(2));
                Assert.That(version.Minor, Is.EqualTo(3));
                Assert.That(version.Patch, Is.EqualTo(10));
            });
        }

        [Test]
        public void PatchVersionUpgradeStrategy_ShouldHandleZeroPatch()
        {
            var version = new VersionInfo { Major = 1, Minor = 2, Patch = 0 };

            _strategy.UpgradeVersion(version);

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(1));
                Assert.That(version.Minor, Is.EqualTo(2));
                Assert.That(version.Patch, Is.EqualTo(1));
            });
        }


        [Test]
        public void PatchVersionUpgradeStrategy_ShouldHandleInitialVersion()
        {
            var version = new VersionInfo();

            _strategy.UpgradeVersion(version);

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(0));
                Assert.That(version.Minor, Is.EqualTo(0));
                Assert.That(version.Patch, Is.EqualTo(1));
            });
        }
                        
    }
}
