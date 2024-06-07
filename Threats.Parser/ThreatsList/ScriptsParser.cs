using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ScriptsParser
{
    public List<Script> Parse(string? scriptsStr, ParsedData data)
    {
        var scripts = new List<Script>();

        if (string.IsNullOrEmpty(scriptsStr))
        {
            return scripts;
        }

        foreach (var scriptString in scriptsStr.Split(';'))
        {
            if (string.IsNullOrWhiteSpace(scriptString))
            {
                continue;
            }

            var identifier = scriptString.Trim();

            var script = data.scripts.Find(s => s.Identifier.Equals(identifier));
            if (script == null)
            {
                script = new Script(data.scripts.Count + 1, identifier);
                data.scripts.Add(script);
            }

            scripts.Add(script);
        }

        return scripts;
    }
}
