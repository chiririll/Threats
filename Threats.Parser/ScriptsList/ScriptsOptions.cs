using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("scripts", HelpText = "Scripts excel file")]
    public string? ScriptsPath { get; private set; }
}
