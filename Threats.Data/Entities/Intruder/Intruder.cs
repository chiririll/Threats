namespace Threats.Data.Entities;

public struct Intruder
{
    public Intruder(IntruderType type, IntruderPotential potential)
    {
        Type = type;
        Potential = potential;
    }

    public IntruderType Type { get; }
    public IntruderPotential Potential { get; }

    public string GetName() => $"(Intruder: {Type}, {Potential})";
}
