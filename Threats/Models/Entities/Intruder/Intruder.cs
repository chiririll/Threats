namespace Threats.Models.Entities;

public class Intruder : Entity
{
    public Intruder(int id, IntruderType type, IntruderPotential potential) : base(id)
    {
        Type = type;
        Potential = potential;
    }

    public IntruderType Type { get; }
    public IntruderPotential Potential { get; }
}
