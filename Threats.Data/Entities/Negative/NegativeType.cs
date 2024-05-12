namespace Threats.Data.Entities;

public class NegativeType : Entity
{
    public NegativeType(long id, string name) : base((int)id)
    {
        Name = name;
    }

    public string Name { get; }
}
