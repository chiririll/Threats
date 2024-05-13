using Newtonsoft.Json;

namespace Threats.Data.Entities;

public readonly struct Intruder
{
    public Intruder(IntruderType type, IntruderPotential potential)
    {
        Type = type;
        Potential = potential;
    }

    [JsonProperty("t")] public IntruderType Type { get; }
    [JsonProperty("p")] public IntruderPotential Potential { get; }

    public string GetName() => $"(Intruder: {Type}, {Potential})";
}
