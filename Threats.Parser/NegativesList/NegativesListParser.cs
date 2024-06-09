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

        var emptyCount = 0;
        var currentType = new NegativeTypeBuilder(data.negativeTypes.Count + 1);

        foreach (var line in File.ReadLines(options.NegativesPath, System.Text.Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                emptyCount++;
                continue;
            }

            if (emptyCount > 1)
            {
                data.negativeTypes.Add(currentType.Build());
                currentType = new NegativeTypeBuilder(data.negativeTypes.Count + 1);
            }
            emptyCount = 0;

            var name = line.Trim();

            if (string.IsNullOrEmpty(currentType.Name))
            {
                currentType.Name = name;
                continue;
            }
            if (string.IsNullOrEmpty(currentType.Description))
            {
                currentType.Description = name;
                continue;
            }

            var negative = new Negative(data.negatives.Count + 1, name, currentType.Id);
            data.negatives.Add(negative);
        }

        data.negativeTypes.Add(currentType.Build());
    }

    private class NegativeTypeBuilder
    {
        public NegativeTypeBuilder(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public NegativeType Build() => new(Id, Name!, Description!);
    }
}
