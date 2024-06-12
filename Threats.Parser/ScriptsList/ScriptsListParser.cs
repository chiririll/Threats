using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using ExcelDataReader;
using Threats.Data.Entities;

namespace Threats.Parser.ScriptsList;

public class ScriptsListParser : IParser
{
    private const int HeaderRowsCount = 1;

    public void Parse(Options options, ParsedData data)
    {
        if (string.IsNullOrWhiteSpace(options.ScriptsPath))
        {
            return;
        }

        var stream = File.Open(options.ScriptsPath, FileMode.Open, FileAccess.Read);
        var reader = ExcelReaderFactory.CreateReader(stream);

        var scriptsData = reader.AsDataSet();

        var scriptsTable = scriptsData.Tables[0];
        ParseScripts(scriptsTable, data);

        reader.Close();
        stream.Close();
    }

    private void ParseScripts(DataTable table, ParsedData data)
    {
        var scriptTypes = new List<ScriptTypeBuilder>();
        var scripts = new List<ScriptBuilder>();

        foreach (var row in table.AsEnumerable().Skip(HeaderRowsCount))
        {
            var typeId = row.Field<string>(Columns.TypeId);
            if (!string.IsNullOrWhiteSpace(typeId) && typeId.Length > 1 && int.TryParse(typeId[1..], out var tId))
            {
                var type = new ScriptTypeBuilder(tId);
                scriptTypes.Add(type);
            }

            if (scriptTypes.Count < 0)
            {
                continue;
            }

            var typeData = row.Field<string>(Columns.Type);
            scriptTypes[^1].AddLine(typeData);

            var line = row.Field<string>(Columns.Scripts);
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (TryParseScript(line, out var script))
            {
                scripts.Add(script);
            }
            else if (scripts.Count > 0)
            {
                scripts[^1].AddLine(line);
            }
            else
            {
                Trace.TraceError($"Failed to parse script line: '{line}'");
            }
        }

        data.scriptTypes.AddRange(scriptTypes.Select(t => t.Build()));
        data.scripts.AddRange(scripts.Select(s => s.Build()));
    }

    private bool TryParseScript(string line, [MaybeNullWhen(false)] out ScriptBuilder builder)
    {
        builder = null;
        var parts = line.Split('.', 3);

        if (parts.Length < 3 || !ScriptId.TryParse(string.Join('.', parts[..2]), out var id))
        {
            return false;
        }

        builder = new(id, parts[2].Trim());
        return true;
    }

    private static class Columns
    {
        public const int TypeId = 0;
        public const int Type = 1;
        public const int Scripts = 2;
    }
}
