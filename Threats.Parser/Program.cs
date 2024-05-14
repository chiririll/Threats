using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Threats.Parser.NegativesLIst;
using Threats.Parser.ThreatsList;

namespace Threats.Parser;

internal sealed class Program
{
    private readonly string outputPath;

    private readonly List<IParser> parsers;

    public Program(string outputPath, string threatsPath, string negativesPath, string objectsPath)
    {
        this.outputPath = outputPath;

        parsers = new()
        {
            new ThreatsListParser(threatsPath),
            new NegativesListParser(negativesPath),
        };
    }

    public static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            return;
        }

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var program = new Program(args[0], args[1], args[2], args[3]);
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

        Save(data, outputPath);

        Exit();
    }

    private static void Save(ParsedData data, string outputPath)
    {
        var json = data.ToEntitiesData().ToJson();

        File.WriteAllText(outputPath, json, System.Text.Encoding.UTF8);
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
