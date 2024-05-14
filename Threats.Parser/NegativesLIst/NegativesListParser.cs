using System.IO;
using Threats.Data.Entities;

namespace Threats.Parser.NegativesLIst;

public class NegativesListParser : IParser
{
    private readonly string path;

    private ParsedData? data;

    public NegativesListParser(string path)
    {
        this.path = path;
    }

    public void Init(ParsedData data)
    {
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
                var type = new NegativeType(data!.negativeTypes.Count + 1, name);
                data.negativeTypes.Add(type);

                hasType = true;
                continue;
            }

            var negative = new Negative(data!.negatives.Count + 1, name, data.negativeTypes.Count);
            data.negatives.Add(negative);
        }
    }
}
