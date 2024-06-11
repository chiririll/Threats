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

            if (!ScriptId.TryParse(scriptString.Trim(), out var id))
            {
                Trace.TraceError($"Failed to parse script id: '{scriptString}'");
                continue;
            }

            if (data.scripts.Find(s => s.Id.Equals(id)) == null)
            {
                Trace.TraceError($"Unknown Script id: '{id}'");
                continue;
            }

            ids.Add(id);
        }

        return ids;
    }
}
