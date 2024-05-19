using CommandLine;

namespace Threats.Parser;

public sealed partial class Options
{
    [Option('e', "entities", Required = true, HelpText = "Output file for entities")]
    public string EntitiesFile { get; private set; } = string.Empty;

    [Option('q', "questions", Required = true, HelpText = "Output file for questions")]
    public string QuestionsFile { get; private set; } = string.Empty;
}
