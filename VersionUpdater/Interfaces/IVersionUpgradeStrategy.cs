
using VersionUpdater.Model;

namespace VersionUpdater.Interfaces
{
    public interface IVersionUpgradeStrategy
    {
        void UpgradeVersion(VersionInfo version);
    }
}
