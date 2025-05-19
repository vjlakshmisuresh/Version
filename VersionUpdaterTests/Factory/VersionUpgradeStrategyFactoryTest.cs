using VersionUpdater.Enums;
using VersionUpdater.Factory;
using VersionUpdater.Strategy;

namespace VersionUpdaterTests.Factory
{
    public class VersionUpgradeStrategyFactoryTest
    {

        [Test]
        public void StrategyFactory_ReturnsMinorStrategy()
        {
            var strategy = VersionUpgradeStrategyFactory.GetVersionUpgradeStrategy(ReleaseType.Minor);
            Assert.That(strategy, Is.InstanceOf<MinorVersionUpgradeStrategy>());
        }

        [Test]
        public void StrategyFactory_ReturnsPatchStrategy()
        {
            var strategy = VersionUpgradeStrategyFactory.GetVersionUpgradeStrategy(ReleaseType.Patch);
            Assert.That(strategy, Is.InstanceOf<PatchVersionUpgradeStrategy>());
        }

        [Test]
        public void StrategyFactory_InvalidReleaseType_ShouldThrowWithCorrectMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => VersionUpgradeStrategyFactory.GetVersionUpgradeStrategy((ReleaseType)99));
            Assert.That(ex.Message, Is.EqualTo("Invalid release type argument"));
        }


    }
}
