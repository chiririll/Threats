using System.IO;
using Threats.Data.Entities;

namespace Threats.Parser.NegativesList;

public class NegativesListParser : IParser
{
    public void Parse(Options options, ParsedData data)
    {
        if (string.IsNullOrWhiteSpace(options.NegativesPath))
        {
            return;
        }

        var hasType = false;

        foreach (var line in File.ReadLines(options.NegativesPath, System.Text.Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                hasType = false;
                continue;
            }

            var name = line.Trim();

            if (!hasType)
            {
                var type = new NegativeType(data!.negativeTypes.Count + 1, name, string.Empty);
                data.negativeTypes.Add(type);

                hasType = true;
                continue;
            }

            var negative = new Negative(data!.negatives.Count + 1, name, data.negativeTypes.Count);
            data.negatives.Add(negative);
        }
    }
}
