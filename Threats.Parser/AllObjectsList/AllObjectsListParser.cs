using System.Diagnostics;
using System.IO;

namespace Threats.Parser.AllObjectsList;

public class AllObjectsListParser : IParser
{
    public void Parse(Options options, ParsedData data)
    {
        if (string.IsNullOrWhiteSpace(options.AllObjectsPath))
        {
            return;
        }

        var i = 0;
        data.objects.Clear();

        foreach (var line in File.ReadAllLines(options.AllObjectsPath, System.Text.Encoding.UTF8))
        {
            i++;

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split('.', 2);

            if (!int.TryParse(parts[0], out var id))
            {
                Trace.TraceError($"Objects List: Failed to parse line {i}");
                continue;
            }

            data.objects.Add(new(id, parts[1].Trim()));
        }
    }
}
