namespace Threats.Models.Entities;

public class Intruder(int id, IntruderType type, IntruderPotential potential) : Entity(id)
{
    public IntruderType Type { get; } = type;
    public IntruderPotential Potential { get; } = potential;
}
