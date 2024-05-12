using System.IO;
using Threats.Data.Entities;

namespace Threats.Parser.NegativesLIst;

public class NegativesListParser
{
    private readonly string path;
    private readonly ThreatsData data;

    public NegativesListParser(string path, ThreatsData data)
    {
        this.path = path;
        this.data = data;
    }

    public void Parse()
    {
        var hasType = false;

        foreach (var line in File.ReadLines(path, System.Text.Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                hasType = false;
                continue;
            }

            var name = line.Trim();

            if (!hasType)
            {
                var type = new NegativeType(data.negativeTypes.Count, name);
                data.negativeTypes.Add(type);

                hasType = true;
                continue;
            }

            var negative = new Negative(data.negatives.Count, name, data.negativeTypes[^1]);
            data.negatives.Add(negative);
        }
    }
}
