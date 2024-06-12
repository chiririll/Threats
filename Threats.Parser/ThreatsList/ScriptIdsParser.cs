using System.Collections.Generic;
using System.Diagnostics;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ScriptIdsParser
{
    public List<ScriptId> Parse(string? scriptsStr, ParsedData data)
    {
        var ids = new List<ScriptId>();

        if (string.IsNullOrEmpty(scriptsStr))
        {
            return ids;
        }

        foreach (var scriptString in scriptsStr.Split(';'))
        {
            if (string.IsNullOrWhiteSpace(scriptString))
            {
                continue;
            }

            var script = scriptString.Trim();

            if (script.Contains('-'))
            {
                var parts = script.Split('-', 2);
                if (parts.Length < 2
                    || !ScriptId.TryParse(parts[0], out var minId)
                    || !ScriptId.TryParse(parts[1], out var maxId))
                {
                    Trace.TraceError($"Failed to parse range script id: '{script}'");
                    continue;
                }

                for (var t = minId.Type; t <= maxId.Type; t++)
                {
                    for (var i = minId.Id; i <= maxId.Id; i++)
                    {
                        CheckAndAddId(new(t, i), ids, data);
                    }
                }

                continue;
            }

            if (!ScriptId.TryParse(script, out var id))
            {
                Trace.TraceError($"Failed to parse script id: '{script}'");
                continue;
            }

            CheckAndAddId(id, ids, data);
        }

        return ids;
    }

    private bool CheckAndAddId(ScriptId id, List<ScriptId> ids, ParsedData data)
    {
        if (data.scripts.Find(s => s.Id.Equals(id)) == null)
        {
            Trace.TraceError($"Unknown Script id: '{id}'");
            return false;
        }

        ids.Add(id);
        return true;
    }
}
