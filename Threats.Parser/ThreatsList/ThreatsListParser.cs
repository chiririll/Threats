using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ThreatsListParser : IParser
{
    private const int HeaderRowsCount = 2;

    private readonly string path;

    private ParsedData? data;
    private IntrudersParser? intrudersParser;
    private ObjectsParser? objectsParser;

    public ThreatsListParser(string path)
    {
        this.path = path;
    }

    public void Init(ParsedData data)
    {
        this.data = data;

        intrudersParser = new();
        objectsParser = new(data);
    }

    public void Parse()
    {
        var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var data = reader.AsDataSet();

        var threatsTable = data.Tables[0];
        ParseThreats(threatsTable);

        reader.Close();
        stream.Close();
    }

    private void ParseThreats(DataTable table)
    {
        var threats = table.AsEnumerable()
            .Skip(HeaderRowsCount)
            .Select(t => ParseThreat(t))
            .Where(t => t != null)
            .Select(t => t!);

        data!.threats.AddRange(threats!);
    }

    private Threat? ParseThreat(DataRow row)
    {
        try
        {
            var id = Convert.ToInt32(row[Columns.Id]);
            var name = row.Field<string>(Columns.Name)!;
            var description = row.Field<string>(Columns.Description)!.Replace("\r", "");

            var intruders = intrudersParser!.Parse(row.Field<string>(Columns.Intruders));
            var objects = objectsParser!.Parse(row.Field<string>(Columns.Objects)).Select(o => o.Id).ToList();

            var violations = SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Columns.Violations.Privacy]) ? SafetyViolations.Privacy : SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Columns.Violations.Integrity]) ? SafetyViolations.Integrity : SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Columns.Violations.Availability]) ? SafetyViolations.Availability : SafetyViolations.None;

            var addDate = row.Field<DateOnly?>(Columns.AddDate, DataRowVersion.Current) ?? default;
            var updateDate = row.Field<DateOnly?>(Columns.UpdateDate, DataRowVersion.Current) ?? default;

            return new Threat(id, name, description, violations, intruders, objects, addDate, updateDate);
        }
        catch (Exception e)
        {
            Trace.TraceError($"Failed to parse row '{row}': {e.Message}");
        }

        return null;
    }

    private static class Columns
    {
        public const int Id = 0;

        public const int Name = 1;
        public const int Description = 2;

        public const int Intruders = 3;
        public const int Objects = 4;

        public const int AddDate = 8;
        public const int UpdateDate = 9;

        public static class Violations
        {
            public const int Privacy = 5;
            public const int Integrity = 6;
            public const int Availability = 7;
        }
    }
}
