

# Name Sorter Application

The Name Sorter is a .NET console application designed to alphabetically sort a list of names by last name, then by given names. This guide provides concise instructions for setup, operation, and testing.

## Features

- **Sorting**: Alphabetically by last name, then given names.
- **SOLID Principles**: Ensures a robust application architecture.
- **Output**: To both the console and a file in the working directory.

## Quick Start

### Prerequisites

- **.NET 8 SDK**: [Download here](https://dotnet.microsoft.com/en-us/download)
- **Visual Studio 2022**: [Download here](https://visualstudio.microsoft.com)

### Setup

1. **Clone**: Clone the repository to your machine.
2. **Open**: Open the solution in Visual Studio 2022.
3. **Set Startup Project**: Designate the console application as the startup project.
4. **Build**: Build the solution to restore and compile.

### Configuration

Configure the output file path in `appsettings.json`:

```json
{
  "AppSettings": {
    "OutputFile": "sorted-names-list.txt"
  }
}
```

### Running the Application

- **From Program Directory**:

Navigate to `NameSorter\bin\Debug\net8.0>` and run:

```shell
namesorter ./input-file-path
```
Example:
```shell
namesorter ./unsorted-names-list.txt
```

- **Using Build.Bat**:

In the `NameSorter` directory, execute:

```shell
build.bat "path-to-your-unsorted-names-list-file"
```
Example:
```shell
build.bat "C:\Projects\NameSorter\TestDataFiles\unsorted-names-list.txt"
```

### Testing

- **.NET CLI**:

```shell
dotnet test
```

- **Visual Studio**:

Use Test Explorer to run all or selected tests.

## Continuous Integration

CI workflows in `.github/workflows` automate build and test processes on each commit, ensuring code quality and integrity.
