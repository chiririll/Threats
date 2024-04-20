namespace Threats.Models.Entities;

public class Object(int id, string name) : Entity(id)
{
    public string Name { get; } = name;
}
