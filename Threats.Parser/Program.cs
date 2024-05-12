using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using Threats.Parser.ThreatsList;

namespace Threats.Parser;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            return;
        }

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        var parser = new ThreatsListParser(args[0]);

        parser.Parse();

        var data = new ThreatsData();
        parser.Fill(data);

        var json = JsonConvert.SerializeObject(data, Formatting.Indented);

        var stream = File.Open(args[1], FileMode.OpenOrCreate, FileAccess.Write);
        var streamWriter = new StreamWriter(stream, System.Text.Encoding.UTF8);

        streamWriter.Write(json);

        streamWriter.Close();
        stream.Close();

        Exit();
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
