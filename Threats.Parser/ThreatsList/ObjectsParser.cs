using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ObjectsParser
{
    private readonly ThreatsData data;

    public ObjectsParser(ThreatsData data)
    {
        this.data = data;
    }

    public List<Object> Parse(string? objectsString)
    {
        var objects = new List<Object>();

        if (string.IsNullOrEmpty(objectsString))
        {
            return objects;
        }

        var separator = objectsString.Contains(';') ? ';' : ',';

        foreach (var objectString in objectsString.Split(separator))
        {
            var name = objectString.Trim();
            var lowered = name.ToLowerInvariant();

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
