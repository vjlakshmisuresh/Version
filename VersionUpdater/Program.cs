using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VersionUpdater.Enums;
using VersionUpdater.Factory;
using VersionUpdater.Interfaces;
using VersionUpdater.Model;
using VersionUpdater.Utility;

var services = new ServiceCollection()
    .AddLogging(config => config.AddConsole())
    .BuildServiceProvider();

var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    if (args.Length != 2)
    {
        logger.LogError("Invalid number of arguments. Usage: dotnet run [minor|patch] [path-to-json]");
        return;
    }

    if (!Enum.TryParse<ReleaseType>(args[0], true, out ReleaseType releaseType))
    {
        throw new ArgumentException($"Invalid release type argument: {args[0]}. Expected minor or patch");
    }

    string filePath = args[1];

    if (!File.Exists(filePath))
    {
        logger.LogError("File does not exist in the specified path: {filePath}", filePath);
        return;
    }

    IVersionUpgradeStrategy versionUpgradeStrategy = VersionUpgradeStrategyFactory.GetVersionUpgradeStrategy(releaseType);

    ReleaseInfo? releaseInfo = ReleaseInfoFileHelper.ReadFromFile(filePath) ?? throw new InvalidOperationException("Release info could not be read.");
    var versionToUpdate = releaseInfo.GetVersion();
    logger.LogInformation("Trying to upgrade the {0} vesion {1}", releaseType, versionToUpdate);
    versionUpgradeStrategy.UpgradeVersion(versionToUpdate);

    ReleaseInfoFileHelper.WriteToFile(filePath, releaseInfo);

    logger.LogInformation("Successfully updated version to: {0}", versionToUpdate);
    return;
}
catch (JsonException jsonEx)
{
    logger.LogError(jsonEx, "Failed to parse JSON file.");
    return;
}
catch (ArgumentException argEx)
{
    logger.LogError(argEx, "Invalid argument.");
    return;
}
catch (IOException ioEx)
{
    logger.LogError(ioEx, "File I/O error.");
    return;
}
catch (Exception ex)
{
    logger.LogError(ex, "Unexpected error occurred.");
    return;
}
