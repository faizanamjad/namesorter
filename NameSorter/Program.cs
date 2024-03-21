using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NameSorter;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Extensions.Options;

/// <summary>
/// An application that takes an argument of a path to a file containing a list of names
/// and sorts those names alphabetically, first by last name, then by given names.
/// Names must have at least one given name and may have up to three given names.
/// Name must have at least one last name.
/// Outputs the sorted list to console and to a file in the working directory.
/// </summary>
/// <param name="args">The command-line arguments.</param>
class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass the file path of unsorted name list as an argument");
                return;
            }

            var host = BuildHost(args);

            // Run the application
            var app = host.Services.GetRequiredService<App>();
            app.Run(args[0]);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"The file was not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine(Environment.NewLine + "Press any key to exit...");
            Console.ReadKey();
        }
    }


    private static IHost BuildHost(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
        .ConfigureServices(ConfigureServices)
        .Build();


    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        var configuration = context.Configuration;
        var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

        services.AddSingleton<INameWriter>(sp =>
        {
            // Composite pattern to write to both file and console
            var writers = new List<INameWriter>
            {
                new FileWriter(appSettings?.OutputFile ?? throw new ArgumentNullException("Output file path cannot be null.")),
                new ConsoleWriter()
            };
            return new NameWriter(writers);
        });
        services.AddSingleton<IFileReader, FileReader>();
        services.AddSingleton<INameReader, NameReader>();
        services.AddSingleton<INameProcessor, NameProcessor>();
        services.AddSingleton<App>();
    }
}
