using System.Collections.Generic;
using Threats.Data;
using Threats.Data.Entities;

namespace Threats.Parser;

public class ParsedData
{
    internal readonly List<Threat> threats = new();
    internal readonly List<Object> objects = new();

    internal readonly List<NegativeType> negativeTypes = new();
    internal readonly List<Negative> negatives = new();

    public EntitiesData ToEntitiesData() => new(threats, objects, negativeTypes, negatives);
}
