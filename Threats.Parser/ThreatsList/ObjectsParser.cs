using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ObjectsParser
{
    public List<Object> Parse(string? objectsString, ParsedData data)
    {
        var objects = new List<Object>();

        if (string.IsNullOrEmpty(objectsString))
        {
            return objects;
        }

        var separator = objectsString.Contains(';') ? ';' : ',';

        foreach (var objectString in objectsString.Split(separator))
        {
            if (string.IsNullOrWhiteSpace(objectString))
            {
                continue;
            }

            var name = objectString.Trim();
            var lowered = name.ToLowerInvariant();

            name = char.ToUpper(name[0]) + name[1..];

            var obj = data.objects.Find(o => o.Name.ToLowerInvariant().Equals(lowered));
            if (obj == null)
            {
                obj = new Object(data.objects.Count + 1, name);
                data.objects.Add(obj);
            }

            objects.Add(obj);
        }

        return objects;
    }
}
