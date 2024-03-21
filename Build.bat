@echo off
setlocal

:: Directory to .NET Core project location
cd NameSorter

:: Build the project
dotnet build

:: Check if a filename was provided as an argument
if "%~1"=="" (
    echo Please provide a file path.
    goto :eof
)

:: Run the application with the file path argument
dotnet run -- %1

:end
endlocal
