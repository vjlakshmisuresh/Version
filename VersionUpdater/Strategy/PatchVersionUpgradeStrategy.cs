using VersionUpdater.Interfaces;
using VersionUpdater.Model;

namespace VersionUpdater.Strategy
{
    public class PatchVersionUpgradeStrategy : IVersionUpgradeStrategy
    {
        public void UpgradeVersion(VersionInfo version)
        {
            version.Patch += 1;
        }
    }
}