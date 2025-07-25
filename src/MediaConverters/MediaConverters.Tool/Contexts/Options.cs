using DotNetCampus.Cli.Compiler;

namespace DotNetCampus.MediaConverters.Contexts;

public class Options
{
    [Option]
    public required string WorkingFolder { get; init; }

    [Option]
    public required string OutputFile { get; init; }

    [Option]
    public required string InputFile { get; init; }

    [Option]
    public required string ConvertConfigurationFile { get; init; }

    public bool? ShouldLogToConsole { get; init; }
    public bool? ShouldLogToFile { get; init; }
}