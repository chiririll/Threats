using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("threats", HelpText = "Threats excel file")]
    public string? ThreatsPath { get; private set; }
}
