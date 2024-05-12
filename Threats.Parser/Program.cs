using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Threats.Parser.NegativesLIst;
using Threats.Parser.ThreatsList;

namespace Threats.Parser;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            return;
        }

        Init();

        var data = Parse(args[0], args[1]);
        Save(data, args[2]);

        Exit();
    }

    private static void Init()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }

    private static ThreatsData Parse(string threatsPath, string negativesPath)
    {
        var data = new ThreatsData();

        var threatsParser = new ThreatsListParser(threatsPath, data);
        var negativesParser = new NegativesListParser(negativesPath, data);

        threatsParser.Parse();
        negativesParser.Parse();

        return data;
    }

    private static void Save(ThreatsData data, string outputPath)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);

        var stream = File.Open(outputPath, FileMode.OpenOrCreate, FileAccess.Write);
        var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

        streamWriter.Write(json);

        streamWriter.Close();
        stream.Close();
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
