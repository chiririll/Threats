namespace Threats.Data.Entities;

public class Object : Entity
{
    public Object(int id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }
}
