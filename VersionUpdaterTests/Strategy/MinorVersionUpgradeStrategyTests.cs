using VersionUpdater.Interfaces;
using VersionUpdater.Model;
using VersionUpdater.Strategy;

namespace VersionUpdaterTests.Strategy
{
    [TestFixture]
    public class MinorVersionUpgradeStrategyTests
    {
        private IVersionUpgradeStrategy _strategy = new MinorVersionUpgradeStrategy();


        [Test]
        public void MinorVersionUpgradeStrategy_ShouldUpgradeMinorResetPatch()
        {
            var version = new VersionInfo { Major = 2, Minor = 3, Patch = 9 };

            _strategy.UpgradeVersion(version);

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(2));
                Assert.That(version.Minor, Is.EqualTo(4));
                Assert.That(version.Patch, Is.EqualTo(0));
            });
        }

        [Test]
        public void MinorVersionUpgradeStrategy_ShouldHandleZeroMinor()
        {
            var version = new VersionInfo { Major = 1, Minor = 0, Patch = 3 };

            _strategy.UpgradeVersion(version);

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(1));
                Assert.That(version.Minor, Is.EqualTo(1));
                Assert.That(version.Patch, Is.EqualTo(0));
            });
        }

        [Test]
        public void MinorVersionUpgradeStrategy_ShouldHandleInitialVersion()
        {
            var version = new VersionInfo();

            _strategy.UpgradeVersion(version);

            Assert.Multiple(() =>
            {
                Assert.That(version.Major, Is.EqualTo(0));
                Assert.That(version.Minor, Is.EqualTo(1));
                Assert.That(version.Patch, Is.EqualTo(0));
            });
        }


    }
}
