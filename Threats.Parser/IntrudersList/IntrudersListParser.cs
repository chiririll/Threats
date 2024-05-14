using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Threats.Data.Entities;

namespace Threats.Parser.IntrudersList;

public class IntrudersListParser : IParser
{
    private const int HeaderRowsCount = 2;

    private readonly string path;

    private ParsedData? data;

    public IntrudersListParser(string path)
    {
        this.path = path;
    }

    public void Init(ParsedData data)
    {
        this.data = data;
    }

    public void Parse()
    {
        var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var data = reader.AsDataSet();

        var objectsTable = data.Tables[0];
        ParseIntruders(objectsTable);

        reader.Close();
        stream.Close();
    }

    private void ParseIntruders(DataTable table)
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

        data!.intruders.AddRange(intruders.Select(i => i.Build()));
    }

    private static class Columns
    {
        public const int Id = 0;
        public const int Title = 1;
        public const int Potential = 2;
        public const int Goals = 3;
    }
}
