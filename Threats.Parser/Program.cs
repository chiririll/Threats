using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Threats.Parser;

internal sealed class Program
{
    private readonly Options options;
    private readonly List<IParser> parsers;

    public Program(Options options)
    {
        this.options = options;

        parsers = new()
        {
            new AllObjectsList.AllObjectsListParser(),
            new ThreatsList.ThreatsListParser(),
            new NegativesList.NegativesListParser(),
            new ObjectsList.ObjectsListParser(),
            new IntrudersList.IntrudersListParser(),
        };
    }

    public static void Main(string[] args)
    {
        if (args.Length < 6)
        {
            return;
        }

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var options = CommandLine.Parser.Default.ParseArguments<Options>(args).Value;
        if (options == null)
        {
            return;
        }

        var program = new Program(options);
        program.Run();
    }

    public void Run()
    {
        var data = new ParsedData();

        foreach (var parser in parsers)
        {
            parser.Parse(options, data);
        }

        Save(data);
        Exit();
    }

    private void Save(ParsedData data)
    {
        var entitiesJson = data.ToEntitiesData().ToJson();
        var questionsJson = data.ToQuestionsData().ToJson();

        File.WriteAllText(options.EntitiesFile, entitiesJson, System.Text.Encoding.UTF8);
        File.WriteAllText(options.QuestionsFile, questionsJson, System.Text.Encoding.UTF8);
    }

    private static void Exit()
    {
        if (!Environment.UserInteractive || string.IsNullOrEmpty(Process.GetCurrentProcess().MainWindowTitle))
        {
            return;
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
