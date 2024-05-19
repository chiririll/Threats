using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("objects", HelpText = "Objects questions excel file")]
    public string? ObjectsPath { get; private set; }
}
