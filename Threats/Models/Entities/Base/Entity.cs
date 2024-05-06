namespace Threats.Models.Entities;

public abstract class Entity
{
    public Entity(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
