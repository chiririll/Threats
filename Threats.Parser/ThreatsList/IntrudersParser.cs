using System.Collections.Generic;
using System.Diagnostics;
using Threats.Data.Entities;

namespace Threats.Parser.ThreatsList;

public class IntrudersParser
{
    public List<Intruder> Parse(string? intrudersString)
    {
        var intruders = new List<Intruder>();

        if (string.IsNullOrEmpty(intrudersString))
        {
            return intruders;
        }

        foreach (var intruderString in intrudersString.Split(';'))
        {
            ParseIntruder(intruderString, intruders);
        }

        return intruders;
    }

    private void ParseIntruder(string intruderString, in List<Intruder> intruders)
    {
        var lowered = intruderString.ToLowerInvariant();

        var type = ParseType(lowered);
        if (type == IntruderType.None)
        {
            Trace.TraceError($"Invalid intruder type: \"{intruderString}\"");
            return;
        }

        var potentials = new List<IntruderPotential>();
        ParsePotential(lowered, potentials);

        if (potentials.Count == 0)
        {
            Trace.TraceError($"Invalid intruder potential: \"{intruderString}\"");
            return;
        }

        foreach (var potential in potentials)
        {
            var intruder = new Intruder(type, potential);

            if (intruders.Contains(intruder))
            {
                continue;
            }

            intruders.Add(intruder);
        }
    }

    private static IntruderType ParseType(string intruderString)
    {
        if (intruderString.Contains("внутр"))
        {
            return IntruderType.Internal;
        }

        if (intruderString.Contains("внешн"))
        {
            return IntruderType.External;
        }

        return IntruderType.None;
    }

    private static void ParsePotential(string intruderString, in List<IntruderPotential> potentials)
    {
        if (intruderString.Contains("низк"))
        {
            potentials.Add(IntruderPotential.Base);
        }

        else if (intruderString.Contains("базов"))
        {
            potentials.Add(intruderString.Contains("повыш")
                ? IntruderPotential.Advanced
                : IntruderPotential.Base);
        }

        else if (intruderString.Contains("средн"))
        {
            potentials.Add(IntruderPotential.Advanced);
            potentials.Add(IntruderPotential.Medium);
        }

        else if (intruderString.Contains("высок"))
        {
            potentials.Add(IntruderPotential.High);
        }
    }
}
