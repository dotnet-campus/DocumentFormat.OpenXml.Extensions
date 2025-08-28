using System.Threading.Tasks;
using DotNetCampus.Cli;
using DotNetCampus.Cli.Compiler;

namespace DotNetCampus.MediaConverters.CommandLineHandlers;

[Verb("convert")]
public class ConvertHandler : ICommandHandler
{
    [Option]
    public required string WorkingFolder { get; init; }

    [Option]
    public required string OutputFile { get; init; }

    [Option]
    public required string InputFile { get; init; }

    [Option]
    public required string ConvertConfigurationFile { get; init; }

    [Option]
    public bool? ShouldLogToConsole { get; init; }

    [Option]
    public bool? ShouldLogToFile { get; init; }

    public async Task<int> RunAsync()
    {
        return await Program.RunAsync(this);
    }
}