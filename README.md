# Introduction

This is a C# Console application that reads content from a JSON file, which contains version information of a release. The application extracts the version information and bumps the version based on the release type input.

# The Task

The task is to create a C# Console application that reads a JSON file containing version information, extracts the version, and increments the version number according to the release type input. The release types supported are `"minor"` and `"patch"`.

# The solution

The solution is implemented using C# and .NET Core, following SOLID principles and Design patterns to ensure clean, extesible and maintainable code.

* The Factory Pattern is used to construct corresponding version upgrading class instance based on the release type.
* Strategy Pattern is used to implement the version bumping logic. Each version bumping class implements the `IVersionUpgradeStrategy` interface, which defines the method for bumping the version.
* The Json file is read using built in JSON library. A custom JSON converter is used to deserialize the version information into a VersionInfo object.

# How to run the application

1. Clone the repository.
1. Open the solution in Visual Studio or any other C# IDE.
1. Build the solution.
1. Run the application using the command line or terminal with the following command
   ```bash
   dotnet run [release_type] [path_to_json_file]
   ```
   where `[release_type]` can be  `minor`, or `patch` and `[path_to_json_file] should be relative or absolute path to the json file`.
1. The application will read the JSON file, and upgrades the version and write it back to the same file.
