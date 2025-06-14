using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("intruders", HelpText = "Intruders questions excel file")]
    public string? IntrudersPath { get; private set; }

    [Option("intruders-negatives", HelpText = "Intruders negative consequences json file")]
    public string? IntrudersNegativesPath { get; private set; }
}
