using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ThreatsListParser
{
    private readonly string path;

    private readonly IntrudersParser intrudersParser;
    private readonly ObjectsParser objectsParser;

    private readonly ThreatsListParserResult result;

    public ThreatsListParser(string path)
    {
        this.path = path;

        result = new();

        intrudersParser = new();
        objectsParser = new(result);
    }

    public void Fill(ThreatsData data)
    {
        data.AddThreats(result.threats);
        data.AddObjects(result.objects);
    }

    public void Parse()
    {
        var stream = File.Open(path, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var data = reader.AsDataSet();

        var threats_table = data.Tables[0];
        ParseThreats(threats_table);

        reader.Close();
        stream.Close();
    }

    private void ParseThreats(DataTable table)
    {
        var threats = table.AsEnumerable()
            .Skip(2)
            .Select(t => ParseThreat(t))
            .Where(t => t != null)
            .Select(t => t!);

        result.threats.AddRange(threats!);
    }

    private Threat? ParseThreat(DataRow row)
    {
        try
        {
            var id = Convert.ToInt32(row[Rows.Id]);
            var name = row.Field<string>(Rows.Name)!;
            var description = row.Field<string>(Rows.Description)!;

            var intruders = intrudersParser.Parse(row.Field<string>(Rows.Intruders));
            var objects = objectsParser.Parse(row.Field<string>(Rows.Objects));

            var violations = SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Rows.Violations.Privacy]) ? SafetyViolations.Privacy : SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Rows.Violations.Integrity]) ? SafetyViolations.Integrity : SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Rows.Violations.Availability]) ? SafetyViolations.Availability : SafetyViolations.None;

            var addDate = row.Field<DateOnly?>(Rows.AddDate, DataRowVersion.Current) ?? default;
            var updateDate = row.Field<DateOnly?>(Rows.UpdateDate, DataRowVersion.Current) ?? default;

            return new Threat(id, name, description, violations, intruders, objects, addDate, updateDate);
        }
        catch (Exception e)
        {
            Trace.TraceError($"Failed to parse row '{row}': {e.Message}");
        }

        return null;
    }

    private static class Rows
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
