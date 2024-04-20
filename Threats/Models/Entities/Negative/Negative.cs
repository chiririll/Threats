namespace Threats.Models.Entities;

public class Negative(int id, string name, NegativeType type) : Entity(id)
{
    public string Name { get; } = name;
    public NegativeType Type { get; } = type;
}
