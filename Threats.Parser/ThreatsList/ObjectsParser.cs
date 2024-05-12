using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ObjectsParser
{
    private readonly ThreatsListParserResult result;

    public ObjectsParser(ThreatsListParserResult result)
    {
        this.result = result;
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

            var obj = result.objects.Find(o => o.Name.ToLowerInvariant().Equals(lowered));
            if (obj == null)
            {
                obj = new Object(result.objects.Count, name);
                result.objects.Add(obj);
            }

            objects.Add(obj);
        }

        return objects;
    }
}
