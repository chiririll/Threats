using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option("intruders", HelpText = "Intruders questions excel file")]
    public string? IntrudersPath { get; private set; }
}
