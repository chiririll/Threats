using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace Threats.Parser.IntrudersList;

public class IntrudersListParser : IParser
{
    private const int HeaderRowsCount = 2;

    public void Parse(Options options, ParsedData data)
    {
        if (string.IsNullOrWhiteSpace(options.IntrudersPath))
        {
            return;
        }

        var stream = File.Open(options.IntrudersPath, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var intrudersData = reader.AsDataSet();

        var intrudersTable = intrudersData.Tables[0];
        var intruders = ParseIntruders(intrudersTable);
        data!.intruders.AddRange(intruders.Select(i => i.Build()));

        reader.Close();
        stream.Close();
    }

    private List<IntruderBuilder> ParseIntruders(DataTable table)
    {
        var intruders = new List<IntruderBuilder>();

        foreach (var row in table.AsEnumerable().Skip(HeaderRowsCount))
        {
            if (row[Columns.Id] is double idDouble)
            {
                var id = System.Convert.ToInt32(idDouble);
                intruders.Add(new IntruderBuilder(id));
            }

            var intruder = intruders[^1];

            intruder.AddTitle(row.Field<string>(Columns.Title));

            intruder.TryAddPotential(row.Field<string>(Columns.Potential));
            intruder.AddGoal(row.Field<string>(Columns.Goals));
        }

        return intruders;
    }

    private static class Columns
    {
        public const int Id = 0;
        public const int Title = 1;
        public const int Potential = 2;
        public const int Goals = 3;
    }
}
