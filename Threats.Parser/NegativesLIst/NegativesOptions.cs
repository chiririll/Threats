using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("negatives", HelpText = "Text file with list of negatives and types")]
    public string? NegativesPath { get; private set; }
}
