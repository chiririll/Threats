namespace Threats.Models.Entities;

public class NegativeType : Entity
{
    public NegativeType(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }
}
