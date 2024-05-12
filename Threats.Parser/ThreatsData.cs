using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser;

public class ThreatsData
{
    private readonly List<Threat> threats = new();
    private readonly List<Object> objects = new();

    public IReadOnlyList<Threat> Threats => threats;
    public IReadOnlyList<Object> Objects => objects;

    internal void AddThreats(IEnumerable<Threat> threats)
    {
        this.threats.AddRange(threats);
    }

    internal void AddObjects(IEnumerable<Object> objects)
    {
        this.objects.AddRange(objects);
    }
}
