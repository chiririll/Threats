namespace Threats.Data.Entities;

public class Negative : Entity
{
    public Negative(int id, string name, int typeId) : base(id)
    {
        Name = name;
        TypeId = typeId;
    }

    public string Name { get; }
    public int TypeId { get; }
}
