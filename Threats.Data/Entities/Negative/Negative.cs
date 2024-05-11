namespace Threats.Models.Entities;

public class Negative : Entity
{
    public Negative(int id, string name, NegativeType type) : base(id)
    {
        Name = name;
        Type = type;
    }

    public string Name { get; }
    public NegativeType Type { get; }
}
