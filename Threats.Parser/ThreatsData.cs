using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser;

public class ThreatsData
{
    internal readonly List<Threat> threats = new();
    internal readonly List<Object> objects = new();

    internal readonly List<NegativeType> negativeTypes = new();
    internal readonly List<Negative> negatives = new();

    public IReadOnlyList<Threat> Threats => threats;
    public IReadOnlyList<Object> Objects => objects;

    public IReadOnlyList<NegativeType> NegativeTypes => negativeTypes;
    public IReadOnlyList<Negative> Negatives => negatives;
}
