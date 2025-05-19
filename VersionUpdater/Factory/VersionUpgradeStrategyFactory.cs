using VersionUpdater.Enums;
using VersionUpdater.Interfaces;
using VersionUpdater.Strategy;

namespace VersionUpdater.Factory
{
    public class VersionUpgradeStrategyFactory
    {
        public static IVersionUpgradeStrategy GetVersionUpgradeStrategy(ReleaseType releaseType)
        {
            return releaseType switch
            {
                ReleaseType.Minor => new MinorVersionUpgradeStrategy(),
                ReleaseType.Patch => new PatchVersionUpgradeStrategy(),
                _ => throw new ArgumentException("Invalid release type argument")
            };

        }
    }
}
