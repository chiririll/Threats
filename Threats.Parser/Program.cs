using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Threats.Parser.NegativesLIst;
using Threats.Parser.ObjectsList;
using Threats.Parser.ThreatsList;

namespace Threats.Parser;

internal sealed class Program
{
    private readonly string entitiesFile;
    private readonly string questionsFile;

    private readonly List<IParser> parsers;

    public Program(string entitiesFile, string questionsFile, string threatsPath, string negativesPath, string objectsPath)
    {
        this.entitiesFile = entitiesFile;
        this.questionsFile = questionsFile;

        parsers = new()
        {
            new ThreatsListParser(threatsPath),
            new NegativesListParser(negativesPath),
            new ObjectsListParser(objectsPath),
        };
    }

    public static void Main(string[] args)
    {
        if (args.Length < 5)
        {
            return;
        }

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var program = new Program(args[0], args[1], args[2], args[3], args[4]);
        program.Run();
    }

    public void Run()
    {
        var data = new ParsedData();

        foreach (var parser in parsers)
        {
            parser.Init(data);
            parser.Parse();
        }

        Save(data);
        Exit();
    }

    private void Save(ParsedData data)
    {
        var entitiesJson = data.ToEntitiesData().ToJson();
        var questionsJson = data.ToQuestionsData().ToJson();

        File.WriteAllText(entitiesFile, entitiesJson, System.Text.Encoding.UTF8);
        File.WriteAllText(questionsFile, questionsJson, System.Text.Encoding.UTF8);
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
