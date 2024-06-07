using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("all-objects", HelpText = "Objects list file")]
    public string? AllObjectsPath { get; private set; }
}
