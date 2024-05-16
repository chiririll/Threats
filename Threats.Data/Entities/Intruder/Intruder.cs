using System;
using Newtonsoft.Json;

namespace Threats.Data.Entities;

public class Intruder : IEquatable<Intruder>
{
    public Intruder(IntruderType type, IntruderPotential potential)
    {
        Type = type;
        Potential = potential;
    }

    [JsonProperty("t")] public IntruderType Type { get; }
    [JsonProperty("p")] public IntruderPotential Potential { get; }

    public bool Equals(Intruder? other) => other != null && Type.Equals(other.Type) && Potential.Equals(other.Potential);
}
