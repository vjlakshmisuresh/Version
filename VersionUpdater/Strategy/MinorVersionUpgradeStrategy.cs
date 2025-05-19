using VersionUpdater.Interfaces;
using VersionUpdater.Model;

namespace VersionUpdater.Strategy
{
    public class MinorVersionUpgradeStrategy : IVersionUpgradeStrategy
    {
        public void UpgradeVersion(VersionInfo version)
        {
            version.Minor += 1;
            version.Patch = 0;
        }
    }
}