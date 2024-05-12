using System.Collections.Generic;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class ThreatsListParserResult
{
    public readonly List<Threat> threats = new();
    public readonly List<Object> objects = new();
}
