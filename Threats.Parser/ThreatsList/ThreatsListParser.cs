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

    private readonly IntrudersParser intrudersParser = new();
    private readonly ObjectsParser objectsParser = new();
    private readonly ScriptIdsParser scriptsParser = new();

    public void Parse(Options options, ParsedData data)
    {
        if (string.IsNullOrWhiteSpace(options.ThreatsPath))
        {
            return;
        }

        var stream = File.Open(options.ThreatsPath, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var threatsData = reader.AsDataSet();

        var threatsTable = threatsData.Tables[0];
        ParseThreats(threatsTable, data);

        reader.Close();
        stream.Close();
    }

    private void ParseThreats(DataTable table, ParsedData data)
    {
        var threats = table.AsEnumerable()
            .Skip(HeaderRowsCount)
            .Select(t => ParseThreat(t, data))
            .Where(t => t != null)
            .Select(t => t!);

        data!.threats.AddRange(threats!);
    }

    private Threat? ParseThreat(DataRow row, ParsedData data)
    {
        try
        {
            var id = Convert.ToInt32(row[Columns.Id]);
            var name = row.Field<string>(Columns.Name)!;
            var description = row.Field<string>(Columns.Description)!.Replace("\r", "");

            var intruders = intrudersParser.Parse(row.Field<string>(Columns.Intruders));
            var objects = objectsParser.Parse(row.Field<string>(Columns.Objects), data).Select(o => o.Id).ToList();
            var scripts = scriptsParser.Parse(row.Field<string>(Columns.Scripts), data);

            var violations = SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Columns.Violations.Privacy]) ? SafetyViolations.Privacy : SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Columns.Violations.Integrity]) ? SafetyViolations.Integrity : SafetyViolations.None;
            violations |= Convert.ToBoolean(row[Columns.Violations.Availability]) ? SafetyViolations.Availability : SafetyViolations.None;

            return new Threat(
                id,
                name,
                description,
                violations,
                intruders,
                objects,
                scripts
            );
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
        public const int Scripts = 8;

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
